using ChatRoomWeb.Data;
using ChatRoomWeb.Repositories;
using NuGet.ProjectModel;
using System.Web.Helpers;

namespace ChatRoomWeb.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserManagementRepository _userManagementRepository;

        public UserManagementService(IUserManagementRepository userManagementRepository)
        {
            _userManagementRepository = userManagementRepository;
        }

        public async Task SignUp(string username, string email, string password, string confirmPassword, ApplicationDbContext _db)
        {
            if (password != confirmPassword)
            {
                Console.WriteLine("bruh");
            }

            var passwordHash = Crypto.HashPassword(password);
            await _userManagementRepository.SignUp(username, email, passwordHash, _db);
        }
    }
}
