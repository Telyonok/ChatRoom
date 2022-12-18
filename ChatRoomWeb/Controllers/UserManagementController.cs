using ChatRoomWeb.Models;
using ChatRoomWeb.Services;
using ChatRoomWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Flurl;
using System.Net.Http.Json;
using NuGet.Protocol;
using Flurl.Http;
using System.Net.Http.Headers;

namespace ChatRoomWeb.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IUserManagementService _userManagementService;

        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ConfirmationReminder()
        {
            return View("ConfirmationReminder");
        }

        public IActionResult SignUp()
        {
            var viewModel = new SignUpViewModel();
            return View("SignUp", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SignUpPostAsync(SignUpViewModel viewModel)
        {
            await _userManagementService.SignUpAsync(viewModel.Username, viewModel.Email, viewModel.Password, viewModel.ConfirmPassword);
            await Task.CompletedTask;
            return RedirectToAction("ConfirmationReminder");
        }

        [HttpPost]
        public async Task<IActionResult> LoginPostAsync(LoginViewModel loginViewModel)
        {
            var httpString = await "https://localhost:7158"
                .AppendPathSegment("/api/token")
                .PostJsonAsync(loginViewModel)
                .ReceiveString();
            var token = httpString[10..^2];
            TokenResponse tokenResponse = new TokenResponse() { Token = token };
            // запрос в authController  = await _authenticationService.RequestTokenAsync(tokenRequest);
            Response.Cookies.Append("X-Access-Token", tokenResponse.Token, 
                new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                });

            return RedirectToAction("ConfirmationReminder");
        }
    }
}
