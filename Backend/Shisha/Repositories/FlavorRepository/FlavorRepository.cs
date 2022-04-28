using Shisha.Models;
using Shisha.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shisha.Repositories
{
    public class FlavorRepository : GenericRepository<Flavor>, IFlavorRepository
    {
        public FlavorRepository(AppDbContext context) : base(context) { }
        public async Task<List<Flavor>> GetAllFlavorsNamePhoto()
        {
            return await _context.Flavors.ToListAsync();
        }

        public async Task<Flavor> GetFlavorWithComments(int id)
        {
            return await _context.Flavors.Include(a => a.Comments).Where(a => a.Id == id).FirstOrDefaultAsync();
        }
    }
}
