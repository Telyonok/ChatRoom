using ChatRoomWeb.Models;
using ChatRoomWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoomWeb.Controllers
{
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("/api/token")]
        public async Task<IActionResult> RequestToken(TokenRequest tokenRequest)
        {
            var token = await _authenticationService.RequestTokenAsync(tokenRequest);
            return Ok(token);
        }
    }
}
