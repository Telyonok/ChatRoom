using ChatRoomWeb.Models;
using ChatRoomWeb.Services;
using ChatRoomWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<IActionResult> RequestToken(LoginViewModel loginViewModel)
        {
            var tokenRequest = new TokenRequest() { Email = loginViewModel.Email, Password = loginViewModel.Password };
            var token = await _authenticationService.RequestTokenAsync(tokenRequest);
            return Ok(token);
        }
    }
}
