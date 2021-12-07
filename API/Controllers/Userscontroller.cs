using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    
    public class Userscontroller : BaseApicontroller
    {
        private readonly ILogger<Userscontroller> _logger;

        public DataContext _context { get; }

        public Userscontroller(DataContext context)
        {
            
            _context = context;
        }


        [Authorize]
        //https://localhost:5001/api/users/
        [HttpGet] 
        //----------------------------------------------
        //https://localhost:5001/api/users/GetAllUsers
        //[HttpGet("GetAllUsers")] if you want to call this method by its name 
        public ActionResult<IEnumerable<AppUser>> GetAllUsers()
        {
            return _context.Users.ToList();
        }


         [AllowAnonymous]
         //https://localhost:5001/api/users/1
         [HttpGet("{id}")]
         //----------------------------------------------
         //[HttpGet("GetUser/{id}")]
         //https://localhost:5001/api/users/GetUser/1
        public ActionResult<AppUser> GetUser(int id)
        {
            return _context.Users.Find(id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}