using Shisha.Models.Entities;
using Shisha.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shisha.Repositories
{
    public interface IUserRepository :IGenericRepository<User>
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUsersByEmail(string email);
        Task<User> GetByIdWithRoles(int id);
    }
}
