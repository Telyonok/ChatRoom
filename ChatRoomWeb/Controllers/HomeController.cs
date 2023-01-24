using ChatRoomWeb.Models;
using ChatRoomWeb.Services;
using ChatRoomWeb.ViewModels;
using Flurl;
using Flurl.Http;
using IdentityModel.Client;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatRoomWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITestService _testService;

        public HomeController(ILogger<HomeController> logger, ITestService testService)
        {
            _logger = logger;
            _testService = testService;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> ProtectedPing()
        {
            var result = await _testService.ProtectedPing();

			var testViewModel = new TestViewModel { Text = result.ToString() };
            ViewBag.Message = testViewModel;
            return View("Test");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            return Problem(title: exception.Message);
        }
    }
}