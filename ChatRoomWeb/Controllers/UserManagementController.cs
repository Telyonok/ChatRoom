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
            var tokenResponse = await _userManagementService.LoginPostAsync(loginViewModel);

            Response.Cookies.Append("X-Access-Token", tokenResponse.Token,
                new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                });

            return RedirectToAction("ConfirmationReminder");
        }

        [Route("VerifyEmail/{verificationData}")]
        public async Task<IActionResult> VerifyEmail(string verificationData)
        {
            var verified = await _userManagementService.VerifyEmail(verificationData);
            if (verified)
            {
                return View("EmailVerified");
            }
            return View("VerifyFailed");
        }
    }
}
