using ChatRoomWeb.Data;

namespace ChatRoomWeb.Repositories
{
    public interface IUserManagementRepository
    {
        Task SignUp(string username, string email, string passwordHash, ApplicationDbContext _db);
    }
}
