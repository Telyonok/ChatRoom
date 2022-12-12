using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoomWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PingController : Controller
    {
        [HttpGet]
        [Route("/ping")]
        public IActionResult Get()
        {
            return Ok(new { Response = "Pong" });
        }

        [HttpGet]
        [Route("/protectedping")]
        [Authorize]
        public IActionResult GetProtectedPing()
        {
            return Ok(new { Response = "Protected Pong" });
        }
    }
}
