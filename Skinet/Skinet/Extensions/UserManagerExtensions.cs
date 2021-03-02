using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Skinet.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindByUserByClamsPrincipleWithAddressAsync(this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            return await input.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);
        }
        public static async Task<AppUser> FindByEmailByClaimsPrincipleAsync(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var email = user?.Claims?.First(x => x.Type == ClaimTypes.Email)?.Value;

            return await userManager.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == email);
           // return await userManager.FindByIdAsync(result.Id);

        }
    }
}
