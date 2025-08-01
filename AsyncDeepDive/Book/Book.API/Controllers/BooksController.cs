using Book.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Book.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookREpository _bookREpository;
    public BooksController(IBookREpository bookREpository)
    {
        _bookREpository = bookREpository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var allBooks = await _bookREpository.GetBooksAsync();
        return Ok(allBooks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAllBooks(int id)
    {
        var book = await _bookREpository.GetBooksAsync(id);
        return Ok(book);
    }

    [HttpGet("/sync")]
    public IActionResult GetAllBooksDefault()
    {
        var allBooks = _bookREpository.GetBooks();
        return Ok(allBooks);
    }
}
