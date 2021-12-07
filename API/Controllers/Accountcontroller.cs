using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    
    public class Accountcontroller : BaseApicontroller
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public Accountcontroller(DataContext context , ITokenService tokenService) 
        {
            _context = context;
            _tokenService = tokenService;
        }


        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username))
            {
                return BadRequest("Username is taken before");
            }
            using(var hmac = new HMACSHA512()){
                
                var user = new AppUser{
                    UserName = registerDto.Username.ToLower(),
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                    PasswordSalt = hmac.Key // 
                };

                 _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return new UserDto{
                    Username = user.UserName,
                    token = _tokenService.CreateToken(user)
                };
            }
            
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = _context.Users.FirstOrDefault(c=>c.UserName == loginDto.Username.ToLower());
            if (user == null)
            {
                return Unauthorized("Invalid username");
            }

            using(var hmac = new HMACSHA512(user.PasswordSalt)){
                
               //get the hash of the written password 
               var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

               for (int i = 0; i < computedHash.Length; i++)
               {
                   if (computedHash[i] != user.PasswordHash[i])
                   {
                       return Unauthorized("Invalid User");
                   }
               }
               return new UserDto{
                    Username = user.UserName,
                    token = _tokenService.CreateToken(user)
                };
            }
            
        }

        private async Task<bool>  UserExists(string Username){

            return  await Task.Run(()=>_context.Users.Any(c=>c.UserName == Username.ToLower()));
        }
        
    }
}