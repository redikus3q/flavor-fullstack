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

namespace Shisha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlavorController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public FlavorController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllFlavors()
        {
            List<Flavor> flavors = await _repository.Flavor.GetAllFlavorsNamePhoto();

            return Ok(new { flavors });
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFlavor(int id)
        {
            Flavor flavor = await _repository.Flavor.GetFlavorWithComments(id);

            return Ok(flavor);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateFlavor(FlavorDTO flavor)
        {
            Flavor newFlavor = new Flavor();

            newFlavor.Name = flavor.Name;
            newFlavor.Price = flavor.Price;
            newFlavor.ImageLink = flavor.ImageLink;
            newFlavor.Description = flavor.Description;
            newFlavor.Comments = new List<Comment>();

            _repository.Flavor.Create(newFlavor);

            await _repository.SaveAsync();

            return Ok(newFlavor);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFlavor(int id)
        {
            var flavor = await _repository.Flavor.GetByIdAsync(id);

            if (flavor == null)
            {
                return NotFound("The flavor doesn't exist.");
            }

            _repository.Flavor.Delete(flavor);

            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditFlavor(int id, FlavorDTO flavor)
        {
            Flavor newFlavor = await _repository.Flavor.GetByIdAsync(id);

            if(newFlavor == null)
            {
                return NotFound("The flavor doesn't exist.");
            }

            newFlavor.Name = flavor.Name;
            newFlavor.Price = flavor.Price;
            newFlavor.ImageLink = flavor.ImageLink;
            newFlavor.Description = flavor.Description;

            await _repository.SaveAsync();

            return Ok(newFlavor);
        }

    }
}
