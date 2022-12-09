using ChatRoomWeb.Data;
using ChatRoomWeb.Models;

namespace ChatRoomWeb.Repositories
{
    public class UserManagementRepository : IUserManagementRepository
    {
        public async Task SignUp(string username, string email, string passwordHash, ApplicationDbContext _db)
        {
            var userSignUp = new UserSignUp(username, email, passwordHash);
            await _db.Users.AddAsync(userSignUp);
            await _db.SaveChangesAsync();
        }
    }
}
