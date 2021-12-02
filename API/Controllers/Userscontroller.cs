using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class Userscontroller : Controller
    {
        private readonly ILogger<Userscontroller> _logger;

        public DataContext _context { get; }

        public Userscontroller(DataContext context)
        {
            
            _context = context;
        }


        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> GetAllUsers()
        {
            return _context.Users.ToList();
        }

         [HttpGet("{id}")]
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