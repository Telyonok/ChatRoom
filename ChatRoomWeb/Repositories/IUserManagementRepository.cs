using ChatRoomWeb.Models;
using ChatRoomWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoomWeb.Repositories
{
    public interface IUserManagementRepository
    {
        Task<int> GetUserIdByVerification(string verificationData);
        Task SignUpAsync(string username, string email, string passwordHash);
        Task<TokenResponse> LoginPostAsync(LoginViewModel loginViewModel);
        Task<bool> IsUniqueUsernameAsync(string userName);
        Task<bool> IsUniqueEmailAsync(string email);
        Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
    }
}
