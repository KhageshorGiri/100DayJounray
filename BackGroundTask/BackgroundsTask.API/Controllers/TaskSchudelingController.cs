using BackGrounds.JOBS.HostedServices;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundsTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskSchudelingController : ControllerBase
    {
        private readonly TestHostServiceJobs _hostedService;
        private readonly ILogger<TaskSchudelingController> _logger;

        public TaskSchudelingController(TestHostServiceJobs hostedService, ILogger<TaskSchudelingController> logger = null)
        {

            _hostedService = hostedService;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> HostedService(CancellationToken cancellationToken)
        {
            await _hostedService.StartAsync(cancellationToken);
            _logger.LogInformation("Hosted service started.");
            return Ok("Hosted service started.");
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> BackGroundService(CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
