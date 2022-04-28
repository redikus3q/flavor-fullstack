using Shisha.Models;
using Shisha.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shisha.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context) { }

        public async Task<List<Comment>> GetCommentsByFlavor(int id)
        {
            return await _context.Comments.Where(a => a.FlavorId == id).ToListAsync();
        }
    }
}
