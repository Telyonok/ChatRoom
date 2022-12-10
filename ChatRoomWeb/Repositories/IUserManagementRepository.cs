using ChatRoomWeb.Data;
using ChatRoomWeb.Models;

namespace ChatRoomWeb.Repositories
{
    public interface IUserManagementRepository
    {
        Task SignUpAsync(string username, string email, string passwordHash);
        Task<User> GetUserByEmailAsync(string email);
    }
}
