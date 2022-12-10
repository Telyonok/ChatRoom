using ChatRoomWeb.Data;
using ChatRoomWeb.Models;
using ChatRoomWeb.Repositories;
using Microsoft.AspNetCore.Identity;
using NuGet.ProjectModel;
using System.Security.Authentication;
using System.Web.Helpers;

namespace ChatRoomWeb.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserManagementRepository _userManagementRepository;

        public UserManagementService(IUserManagementRepository userManagementRepository)
        {
            _userManagementRepository = userManagementRepository;
        }

        public async Task<User> LoginAsync(TokenRequest login)
        {
            var user = await _userManagementRepository.GetUserByEmailAsync(login.Email);
            if (user == null)
            {
                throw new AuthenticationException();
            }

            var isValid = Crypto.VerifyHashedPassword(user.PasswordHash, login.Password);
            if (!isValid)
            {
                throw new AuthenticationException();
            }

            return user;
        }

        public async Task SignUpAsync(string username, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                Console.WriteLine("bruh");
                return;
            }

            var passwordHash = Crypto.HashPassword(password);
            await _userManagementRepository.SignUpAsync(username, email, passwordHash);
        }
    }
}
