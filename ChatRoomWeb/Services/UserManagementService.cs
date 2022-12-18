using ChatRoomWeb.Helpers;
using ChatRoomWeb.Models;
using ChatRoomWeb.Repositories;
using System.Security.Authentication;


namespace ChatRoomWeb.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserManagementRepository _userManagementRepository;

        public UserManagementService(IUserManagementRepository userManagementRepository)
        {
            _userManagementRepository = userManagementRepository;
        }

        public async Task SignUpAsync(string username, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                Console.WriteLine("bruh");
                return;
            }

            var passwordHash = Crypto.HashPassword(password);
            await _userManagementRepository.SignUpAsync(username, email, passwordHash);
        }
    }
}
