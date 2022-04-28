using Shisha.Models;
using Shisha.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shisha.Repositories
{
    public class SessionTokenRepository : GenericRepository<SessionToken>, ISessionTokenRepository
    {
        public SessionTokenRepository(AppDbContext context) : base(context) { }

        public async Task<SessionToken> GetByJTI(string jti)
        {
            return await _context.SessionTokens.FirstOrDefaultAsync(t => t.Jti.Equals(jti));
        }
    }
}
