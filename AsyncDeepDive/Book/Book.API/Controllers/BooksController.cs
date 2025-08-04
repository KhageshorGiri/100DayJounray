using Book.API.Entities;
using Book.API.Models;
using Book.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Book.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookREpository _bookREpository;
    private readonly IUriService _uriService;
    public BooksController(IBookREpository bookREpository, IUriService uriService)
    {
        _bookREpository = bookREpository;
        _uriService = uriService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks([FromQuery] PaginationQuery query)
    {
        var allBooks = await _bookREpository.GetBooksAsync(query);

        return Ok(new PaginatedApiResponse<IEnumerable<Books>>(allBooks));
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
