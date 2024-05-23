using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> Index()
        {
            return Ok("Hello form index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> AdminIndex()
        {
            return Ok("Hello form admin");
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> UserIndex()
        {
            return Ok("Hello form user");
        }
    }
}
