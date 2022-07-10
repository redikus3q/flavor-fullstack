using Shisha.Models.DTOs;
using Shisha.Models.Entities;
using Shisha.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Shisha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly UserManager<User> _userManager;
        public CommentController(IRepositoryWrapper repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> CreateComment(CommentDTO comment)
        {
            Comment newComment = new Comment();

            int flavorId = comment.FlavorId;

            int id;
            bool success = int.TryParse(_userManager.GetUserId(HttpContext.User), out id);

            if (!success)
            {
                return BadRequest("User's ID is not proper.");
            }
            

            Flavor flavor = await _repository.Flavor.GetByIdAsync(flavorId);
            User user = await _repository.User.GetByIdAsync(id);

            newComment.Text = comment.Text;
            newComment.FlavorId = flavor.Id;
            newComment.Flavor = flavor;
            newComment.UserId = user.Id;
            newComment.User = user;
            newComment.Date = DateTime.Now;

            _repository.Comment.Create(newComment);

            flavor.Comments.Add(newComment);

            await _repository.SaveAsync();

            return Ok(newComment);
        }

        [HttpGet("{flavorId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCommentsById(int flavorId)
        {
            List<Comment> comments = await _repository.Comment.GetCommentsByFlavor(flavorId);

            for(int i = 0; i < comments.Count; i++)
            {
                int id = comments[i].UserId;
                comments[i].User = await _repository.User.GetByIdAsync(id);
            }

            return Ok(new { comments });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var user = HttpContext.User;
            int userId;
            bool successId = int.TryParse(_userManager.GetUserId(user), out userId);
            bool isAdmin = user.IsInRole("Admin");

            if (!successId)
            {
                return BadRequest("User's ID is not proper.");
            }

            var comment = await _repository.Comment.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound("The comment doesn't exist.");
            }

            int commentUserId = comment.UserId;

            if (commentUserId != userId && isAdmin == false)
            {
                return BadRequest("Cannot delete another user's comment!");
            }

            _repository.Comment.Delete(comment);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}

// a : String1!
