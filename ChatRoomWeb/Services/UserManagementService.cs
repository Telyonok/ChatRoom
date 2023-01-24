using Azure;
using ChatRoomWeb.Helpers;
using ChatRoomWeb.Models;
using ChatRoomWeb.Repositories;
using ChatRoomWeb.ViewModels;
using Flurl;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;


namespace ChatRoomWeb.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserManagementRepository _userManagementRepository;

        public UserManagementService(IUserManagementRepository userManagementRepository)
        {
            _userManagementRepository = userManagementRepository;
        }

        public async Task<bool> IsUniqueEmailAsync(string email)
        {
            return await _userManagementRepository.IsUniqueEmailAsync(email);
        }

        public async Task<bool> IsUniqueUsernameAsync(string username)
        {
            return await _userManagementRepository.IsUniqueUsernameAsync(username);
        }

        public async Task<TokenResponse> LoginPostAsync(LoginViewModel loginViewModel)
        {
            return await _userManagementRepository.LoginPostAsync(loginViewModel);
        }

        public async Task<TokenResponse> RefreshTokenAsync(string refreshToken)
        {
            return await _userManagementRepository.RefreshTokenAsync(new RefreshTokenRequest { RefreshToken = refreshToken});
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

        public async Task<bool> VerifyEmail(string verificationData)
        {
            var userId = await _userManagementRepository.GetUserIdByVerification(verificationData);
            return userId > 0;
        }
    }
}
