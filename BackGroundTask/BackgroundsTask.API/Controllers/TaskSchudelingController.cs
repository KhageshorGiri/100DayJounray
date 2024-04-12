using Microsoft.AspNetCore.Mvc;

namespace BackgroundsTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskSchudelingController : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> HostedService(CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> BackGroundService(CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
