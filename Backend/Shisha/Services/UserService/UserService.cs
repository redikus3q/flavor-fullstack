using Shisha.Models.Constants;
using Shisha.Models.DTOs;
using Shisha.Models.Entities;
using Shisha.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shisha.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly UserManager<User> _userManager;

        public UserService(
            UserManager<User> userManager,
            IRepositoryWrapper repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        public async Task<bool> RegisterUserAsync(RegisterUserDTO dto)
        {
            var registerUser = new User();

            registerUser.Email = dto.Email;
            registerUser.UserName = dto.Email;
            registerUser.FirstName = dto.FirstName;
            registerUser.LastName = dto.LastName;

            var result = await _userManager.CreateAsync(registerUser, dto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(registerUser, UserRoleType.User);

                return true;
            }

            return false;
        }

        public async Task<string> LoginUser(LoginUserDTO dto)
        {
            User user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                return null;
            }

            var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            user = await _repository.User.GetByIdWithRoles(user.Id);
            List<string> roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();

            var newJti = Guid.NewGuid().ToString();
            var tokenHandler = new JwtSecurityTokenHandler();
            var signinkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom secret key for auth"));

            var token = GenerateJwtToken(signinkey, user, roles, tokenHandler, newJti);

            _repository.SessionToken.Create(new SessionToken(newJti, user.Id, token.ValidTo));
            await _repository.SaveAsync();

            return tokenHandler.WriteToken(token);
        }

        private SecurityToken GenerateJwtToken(SymmetricSecurityKey signinkey, User user, List<string> roles, JwtSecurityTokenHandler tokenHandler, string newJti)
        {

            var subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, newJti)
            });

            foreach (var role in roles)
            {
                subject.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var expiryDate = DateTime.Now.AddDays(1);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expiryDate,
                SigningCredentials = new SigningCredentials(signinkey, SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return token;
        }

        public async Task<Boolean> FindToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            var jti = jsonToken.Claims.First(claim => claim.Type == "jti").Value;

            SessionToken response = await _repository.SessionToken.GetByJTI(jti);

            if(response == null)
            {
                return false;
            }

            if(response.ExpirationDate < DateTime.Now)
            {
                return false;
            }

            return true;
        }
    }

    //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImEiLCJ1bmlxdWVfbmFtZSI6IlN1bHRhbnVsIFNpdGV1bHVpIiwibmFtZWlkIjoiMSIsImp0aSI6ImY4MWY5MGI2LWNlNzEtNGFiNC1iM2ExLTFhMTM0MjFmMDg3NiIsInJvbGUiOiJBZG1pbiIsIm5iZiI6MTY0NzkwNjkxNCwiZXhwIjoxNjQ3OTkzMzE0LCJpYXQiOjE2NDc5MDY5MTR9.ITbMtd2bN-AztOBJnD8TjZBN775NDmkANlf_CAl8xwk
}
