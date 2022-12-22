using ChatRoomWeb.Models;
using ChatRoomWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoomWeb.Repositories
{
    public interface IUserManagementRepository
    {
        Task<int> GetUserIdByVerification(string verificationData);
        Task SignUpAsync(string username, string email, string passwordHash);
        public Task<string> LoginPostAsync(LoginViewModel loginViewModel);
    }
}
