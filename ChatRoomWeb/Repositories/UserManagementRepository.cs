using ChatRoomWeb.Models;
using Flurl;
using Flurl.Http;

namespace ChatRoomWeb.Repositories
{
    public class UserManagementRepository : IUserManagementRepository
    {
        public async Task SignUpAsync(string username, string email, string passwordHash)
        {
            var userSignUp = new User(username, email, passwordHash);

            await "https://localhost:7158"
                .AppendPathSegment("/api/signup")
                .PostJsonAsync(userSignUp);
        }
    }
}
