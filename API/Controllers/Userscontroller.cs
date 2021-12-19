using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Authorize]
    public class Userscontroller : BaseApicontroller
    {
        private readonly ILogger<Userscontroller> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public Userscontroller(IUserRepository userRepository , IMapper mapper)
        {
            
           
            _userRepository = userRepository;
            _mapper = mapper;
        }


        //[Authorize]
        //https://localhost:5001/api/users/
        [HttpGet] 
        //----------------------------------------------
        //https://localhost:5001/api/users/GetAllUsers
        //[HttpGet("GetAllUsers")] if you want to call this method by its name 
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetAllUsers()
        {
            // var users =await _userRepository.GetUsersAsync();
            // var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
           // return Ok(usersToReturn);
           return Ok(await _userRepository.GetMemebersAsync());
        }


         //[AllowAnonymous]
         //https://localhost:5001/api/users/1
         [HttpGet("{username}")]
         //----------------------------------------------
         //[HttpGet("GetUser/{id}")]
         //https://localhost:5001/api/users/GetUser/1
        public async Task<ActionResult<MemberDto>> GetUser(String username)
        { 
            // var user =  await _userRepository.GetUserByUsernameAsync(username);
            // return _mapper.Map<MemberDto>(user);
            return await _userRepository.GetMemeberAsync(username);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}