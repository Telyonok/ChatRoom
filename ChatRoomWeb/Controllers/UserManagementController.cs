using ChatRoomWeb.Data;
using ChatRoomWeb.Models;
using ChatRoomWeb.Services;
using ChatRoomWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Flurl;
using System.Net.Http.Json;
using NuGet.Protocol;

namespace ChatRoomWeb.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IUserManagementService _userManagementService;
        private readonly IAuthenticationService _authenticationService;

        public UserManagementController(IUserManagementService userManagementService, IAuthenticationService authenticationService)
        {
            _userManagementService = userManagementService;
            _authenticationService = authenticationService;
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
            var tokenRequest = new TokenRequest() { Email = loginViewModel.Email, Password = loginViewModel.Password };
            var tokenResponse = await _authenticationService.RequestTokenAsync(tokenRequest);
            Response.Cookies.Append("X-Access-Token", tokenResponse.Token, 
                new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                });

            return RedirectToAction("GetProtectedPing", "Ping");
        }
    }
}
