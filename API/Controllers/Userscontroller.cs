using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IPhotoService _photoService;

        public Userscontroller(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {


            _userRepository = userRepository;
            _mapper = mapper;
            _photoService = photoService;
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
        [HttpGet("{username}", Name = "GetUser")]
        //----------------------------------------------
        //[HttpGet("GetUser/{id}")]
        //https://localhost:5001/api/users/GetUser/1
        public async Task<ActionResult<MemberDto>> GetUser(String username)
        {
            // var user =  await _userRepository.GetUserByUsernameAsync(username);
            // return _mapper.Map<MemberDto>(user);
            return await _userRepository.GetMemeberAsync(username);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            //var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            _mapper.Map(memberUpdateDto, user);
            if (await _userRepository.SaveAllAsync())
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Failed to update user");
            }


        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            var result = await _photoService.AddPhotoAsync(file);
            if (result.Error != null)
            {
                return BadRequest(result.Error.Message);
            }
            var photo = new Photo() { Url = result.SecureUrl.AbsoluteUri, PublicId = result.PublicId };
            if (user.Photos.Count == 0)
            {
                photo.IsMain = true;
            }
            user.Photos.Add(photo);
            if (await _userRepository.SaveAllAsync())
            {
                // return _mapper.Map<PhotoDto>(photo);
                return CreatedAtRoute("GetUser", new { username = user.UserName }, _mapper.Map<PhotoDto>(photo));// to get 201Created in postman
                                                                                                                 //(name of the rout to go to after success , paraneters of this rout , object of the result)
            }
            return BadRequest("problem Adding Photo");

        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            var photo = user.Photos.FirstOrDefault(c => c.Id == photoId);
            if (photo.IsMain)
            {
                return BadRequest("this is already the main Photo");

            }

            var currentMain = user.Photos.FirstOrDefault(c => c.IsMain = true);
            if (currentMain != null)
            {
                currentMain.IsMain = false;
            }
            photo.IsMain = true;
            if (await _userRepository.SaveAllAsync())
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Failed to set main Photo");
            }

        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("You cannot delete your main photo");

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            user.Photos.Remove(photo);

            if (await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete the photo");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}