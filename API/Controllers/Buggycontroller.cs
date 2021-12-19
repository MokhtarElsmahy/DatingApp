using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Data;
using Microsoft.AspNetCore.Authorization;
using API.Entities;

namespace API.Controllers
{
    
    public class Buggycontroller : BaseApicontroller
    {
        private readonly ILogger<Buggycontroller> _logger;
        private readonly DataContext _context;

        public Buggycontroller(ILogger<Buggycontroller> logger , DataContext context )
        {
            _logger = logger;
            _context = context;
        }


        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {

            return "secret text";
        }

         [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _context.Users.Find(-1);
            if (thing==null)
            {
                return NotFound();
            }
            return Ok(thing);
        }
         [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var thing = _context.Users.Find(-1);
            var thingToReturn = thing.ToString();
            return thingToReturn;
        }
         [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("this is not a good request");
        }

       
    }
}