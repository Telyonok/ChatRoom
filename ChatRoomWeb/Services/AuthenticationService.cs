using ChatRoomWeb.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace ChatRoomWeb.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private const string JwtSigningKey = "verysecretstuff_don't_l00k";
        private readonly IUserManagementService _userManagementService;
        public AuthenticationService(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        public async Task<TokenResponse> RequestTokenAsync(TokenRequest tokenRequest)
        {
            var user = await _userManagementService.LoginAsync(tokenRequest);
            if (user != null)
            {
                var claim = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSigningKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken
                    (
                    issuer: "localhost:7225",
                    audience: "localhost:7225",
                    claims: claim,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                    );

                return new TokenResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
            }

            throw new AuthenticationException();
        }
    }
}
