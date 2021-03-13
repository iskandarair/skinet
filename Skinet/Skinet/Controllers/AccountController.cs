using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Skinet.Dtos;
using Skinet.Errors;
using Skinet.Extensions;
using Skinet.Helpers;

namespace Skinet.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapping)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapping;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new ApiResponse(401));
            }

            return new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterAsync(RegisterDto registerDto)
        {
            if (CheckEmailExists(registerDto.Email).Result)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse{Errors = new []{ "the email address is already in use" }});
            }
            var user = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            };
        }

        [HttpGet("emailexists")]
        public async Task<bool> CheckEmailExists([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddresDto>> GetUserAddress()
        {
            var user = await _userManager.FindByUserByClamsPrincipleWithAddressAsync(HttpContext.User);
            
            return _mapper.Map<Address, AddresDto>(user.Address);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddresDto>> UpdateUserAddressAsync(AddresDto addressDto)
        {
            try
            {
                var user = await _userManager.FindByEmailByClaimsPrincipleAsync(HttpContext.User);

                user.Address = _mapper.Map<AddresDto, Address>(addressDto);
                var result = await  _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return Ok(_mapper.Map<Address, AddresDto>(user.Address));
                }

                return BadRequest("Problem updating the user");
            }
            catch (Exception ex)
            {

                return BadRequest("Error"+ ex.Message +  ex.StackTrace);
            }

        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailByClaimsPrincipleAsync(HttpContext.User);
            return  new UserDto()
                {
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                    DisplayName = user.DisplayName
                }
;        }
    }
}
