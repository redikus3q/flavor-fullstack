using Shisha.Models.DTOs;
using Shisha.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shisha.Services.UserServices
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(RegisterUserDTO dto);
        Task<string> LoginUser(LoginUserDTO dto);
        Task<Boolean> FindToken(string token);
    }
}
