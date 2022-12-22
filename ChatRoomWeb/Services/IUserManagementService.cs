using ChatRoomWeb.Models;
using ChatRoomWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoomWeb.Services
{
    public interface IUserManagementService
    {
        Task SignUpAsync(string username, string email,
            string password, string confirmPassword);
        Task<bool> VerifyEmail(string verificationData);
        public Task<TokenResponse> LoginPostAsync(LoginViewModel loginViewModel);
    }
}
