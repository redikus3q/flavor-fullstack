using Shisha.Models;
using Shisha.Models.Constants;
using Shisha.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shisha.Seed
{
    public class SeedDb
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly AppDbContext _context;

        public SeedDb(
            RoleManager<Role> roleManager,
            AppDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public async Task SeedRoles()
        {
            if (_context.Roles.Any())
            {
                return;
            }

            string[] roleNames =
            {
                UserRoleType.Admin,
                UserRoleType.User
            };

            foreach (var roleName in roleNames)
            {
                var roleExists = await _roleManager.RoleExistsAsync(roleName);

                if (!roleExists)
                {
                    var roleResult = await _roleManager.CreateAsync(new Role
                    {
                        Name = roleName
                    });
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
