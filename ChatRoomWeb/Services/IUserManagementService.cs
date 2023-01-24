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
        Task<TokenResponse> LoginPostAsync(LoginViewModel loginViewModel);
        Task<bool> IsUniqueUsernameAsync(string userName);
        Task<bool> IsUniqueEmailAsync(string email);
        Task<TokenResponse> RefreshTokenAsync(string refreshToken);
    }
}
