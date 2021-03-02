using Infrastrcuture.Data;
using Microsoft.AspNetCore.Mvc;
using Skinet.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Skinet.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context; 
        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoudResult()
        {
            var result = _context.Products.Find(344);
            if(result == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }

        [HttpGet("authTest")]
        [Authorize]
        public ActionResult<string> GetAuthTest()
        {
            return "Authorized, you are";
        }

        [HttpGet("serverError")]
        public ActionResult GetServerError()
        {
            var result = _context.Products.Find(344);
            result.Description = "error";
            if (result == null)
            {
                return NotFound();
            }
            return Ok();

        }

        [HttpGet("badRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }

        [HttpGet("badRequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }

    }
}
