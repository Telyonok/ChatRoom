using ChatRoomWeb.Data;
using ChatRoomWeb.Models;

namespace ChatRoomWeb.Services
{
    public interface IUserManagementService
    {
        Task SignUpAsync(string username, string email,
            string password, string confirmPassword);
        Task<User> LoginAsync(TokenRequest tokenRequest);
    }
}
