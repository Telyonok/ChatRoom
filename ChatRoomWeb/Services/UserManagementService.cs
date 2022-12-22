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

        public async Task<TokenResponse> LoginPostAsync(LoginViewModel loginViewModel)
        {
            var httpString = await _userManagementRepository.LoginPostAsync(loginViewModel);
            var token = httpString[10..^2];
            TokenResponse tokenResponse = new TokenResponse() { Token = token };
            return tokenResponse;
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
