using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SharedModel;

namespace Producer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProducerController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;
    public ProducerController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return Ok("hello");
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(Order order)
    {
        await Task.Delay(1000);
        await _publishEndpoint.Publish(order);
        return Created();
    }
}
