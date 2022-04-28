using Shisha.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shisha.Repositories
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        ISessionTokenRepository SessionToken { get; }
        IFlavorRepository Flavor { get; }
        ICommentRepository Comment { get; }

        Task SaveAsync();
    }
}
