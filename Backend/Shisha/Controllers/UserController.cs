using Shisha.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shisha.Models.Entities;

namespace Shisha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public UserController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _repository.User.GetAllUsers();

            return Ok(new { users });
        }

        [HttpGet("{id}")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetUser(int id)
        {
            User user = await _repository.User.GetByIdAsync(id);

            return Ok(user);
        }

    }
    ////[Authorize(Roles = "Admin")]
    //[Authorize] // verifica sa vada daca e logat, merge orice rol daca e logat
    //    //[Authorize(Roles = "Admin")] // asa cu mai multe
    //    //[AllowAnonymous] // nu necesita autorizare - e by default asa dar l-am pus sanki
    //    public async Task<IActionResult> GetAllUsers()
    //    {
    //        var users = await _repository.User.GetAllUsers();

    //        return Ok(new { users });
    //    }
    //}
}
