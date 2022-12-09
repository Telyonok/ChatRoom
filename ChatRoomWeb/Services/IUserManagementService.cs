using ChatRoomWeb.Data;

namespace ChatRoomWeb.Services
{
    public interface IUserManagementService
    {
        Task SignUp(string username, string email,
            string password, string confirmPassword, ApplicationDbContext _db);
    }
}
