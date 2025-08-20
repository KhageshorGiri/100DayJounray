using Microsoft.AspNetCore.Mvc;

namespace Producer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProducerController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return Ok("hello");
    }
}
