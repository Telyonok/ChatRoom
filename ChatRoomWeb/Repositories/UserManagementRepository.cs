using ChatRoomWeb.Models;
using ChatRoomWeb.ViewModels;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ChatRoomWeb.Repositories
{
    public class UserManagementRepository : RepositoryBase, IUserManagementRepository
    {
        public UserManagementRepository(IFlurlClientFactory flurlClientFactory, IHttpContextAccessor httpContextAccessor):base(flurlClientFactory, httpContextAccessor)
        {
        }

        public async Task<int> GetUserIdByVerification(string verificationData)
        {
            var verifyEmailResponse = await _flurlClient
                                    .Request($"/api/VerifyEmail/{verificationData}") 
                                    .GetJsonAsync<VerifyEmailResponse>();
            if (verifyEmailResponse == null)
            {
                return -1;
            }

            return verifyEmailResponse.UserId;
        }

        public async Task<bool> IsUniqueEmailAsync(string email)
        {
            return await _flurlClient
                .Request($"/api/IsUniqueEmail/{email}")
                .GetJsonAsync<bool>();
        }

        public async Task<bool> IsUniqueUsernameAsync(string username)
        {
            return await _flurlClient
                .Request($"/api/IsUniqueUsername/{username}")
                .GetJsonAsync<bool>();
        }

        public async Task<TokenResponse> LoginPostAsync(LoginViewModel loginViewModel)
        {
            return await _flurlClient
                .Request("/api/token")
                .PostJsonAsync(loginViewModel)
                .ReceiveJson<TokenResponse>();
        }

        public async Task SignUpAsync(string username, string email, string passwordHash)
        {
            var userSignUp = new User(username, email, passwordHash);

            await _flurlClient
                .Request("/api/signup")
                .PostJsonAsync(userSignUp);
        }

        public async Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
        {
            return await _flurlClient
                .Request("/api/refreshToken")
                .PostJsonAsync(refreshTokenRequest)
                .ReceiveJson<TokenResponse>();
        }
    }
}
