using ChatRoomWeb.Models;

namespace ChatRoomWeb.Services
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> RequestTokenAsync(TokenRequest tokenRequest);
    }
}
