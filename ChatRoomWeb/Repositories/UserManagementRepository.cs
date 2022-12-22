using ChatRoomWeb.Models;
using ChatRoomWeb.ViewModels;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ChatRoomWeb.Repositories
{
    public class UserManagementRepository : IUserManagementRepository
    {
        public async Task<int> GetUserIdByVerification(string verificationData)
        {
            var verifyEmailResponse = await "https://localhost:7158"
                .AppendPathSegment($"/api/VerifyEmail/{verificationData}")
                .AllowHttpStatus(HttpStatusCode.OK, HttpStatusCode.NotFound)
                .GetJsonAsync<VerifyEmailResponse>();
            if (verifyEmailResponse == null)
            {
                return -1;
            }

            return verifyEmailResponse.UserId;
        }

        public async Task<string> LoginPostAsync(LoginViewModel loginViewModel)
        {
            return "https://localhost:7158"
                .AppendPathSegment("/api/token")
                .PostJsonAsync(loginViewModel)
                .ReceiveString().Result;
        }

        public async Task SignUpAsync(string username, string email, string passwordHash)
        {
            var userSignUp = new User(username, email, passwordHash);

            await "https://localhost:7158"
                .AppendPathSegment("/api/signup")
                .PostJsonAsync(userSignUp);
        }
    }
}
