using Microsoft.AspNetCore.Mvc;

namespace Book.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    public BooksController()
    {
        
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        return Ok("hello all");
    }
}
