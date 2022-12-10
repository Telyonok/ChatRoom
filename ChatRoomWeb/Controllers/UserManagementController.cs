using ChatRoomWeb.Data;
using ChatRoomWeb.Services;
using ChatRoomWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

namespace ChatRoomWeb.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IUserManagementService _userManagementService;
        public readonly ApplicationDbContext _db;

        public UserManagementController(IUserManagementService userManagementService, ApplicationDbContext db)
        {
            _userManagementService = userManagementService;
            _db = db;
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
    }
}
