using Shisha.Models.Constants;
using Shisha.Models.DTOs;
using Shisha.Models.Entities;
using Shisha.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shisha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public AccountController(
            UserManager<User> userManager,
            IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user != null)
            {
                return BadRequest("There already is an user registered with that email!");
            }

            var result = await _userService.RegisterUserAsync(dto);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
        {
            var token = await _userService.LoginUser(dto);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }

        [HttpPost("checkToken")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckToken([FromBody] SessionTokenDTO token)
        {
            var response = await _userService.FindToken(token.Token);

            return Ok(response);
        }

        [HttpGet("getUser")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser()
        {
            var response = await _userManager.GetUserAsync(HttpContext.User);
            return Ok(response);
        }

        [HttpGet("isAdmin")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult IsAdmin()
        {
            var user = HttpContext.User;
            bool isAdmin = user.IsInRole("Admin");
            return Ok(isAdmin);
        }
    }
}
