using Shisha.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shisha.Repositories
{
    public interface ISessionTokenRepository : IGenericRepository<SessionToken>
    {
        Task<SessionToken> GetByJTI(string jti);
    }
}
