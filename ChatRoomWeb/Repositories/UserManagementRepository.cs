using ChatRoomWeb.Data;
using ChatRoomWeb.Models;

namespace ChatRoomWeb.Repositories
{
    public class UserManagementRepository : IUserManagementRepository
    {
        private readonly IServiceScopeFactory scopeFactory;

        public UserManagementRepository(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var user = _db.Users.Where(x => x.Email == email).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }

                return null;
            }
        }

        public async Task SignUpAsync(string username, string email, string passwordHash)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userSignUp = new User(username, email, passwordHash);
                await _db.Users.AddAsync(userSignUp);
                await _db.SaveChangesAsync();
            }
        }
    }
}
