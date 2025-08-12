using Book.API.Entities;
using Book.API.Models;
using Book.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Text.Json;

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

    [HttpGet(Name = "GetAllBooks")]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client, NoStore = false)]
    //[OutputCache(Duration = 60, NoStore = false)]
    public async Task<IActionResult> GetAllBooks([FromQuery] BookResourceDto query)
    {
        var allBooks = await _bookREpository.GetBooksAsync(query);

        var paginationMetadata = new
        {
            totalCount = allBooks.TotalCount,
            pageSize = allBooks.PageSize,
            currentPage = allBooks.CurrentPage,
            totalPages = allBooks.TotalPages,
        };

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

        // create links
        var links = CreateLinksForBooks(query, allBooks.HasNext, allBooks.HasPrevious);

        var response = new
        {
            values = allBooks,
            links = links
        };

        return Ok(response);
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

    #region PRIVET METHODS

    private IEnumerable<LinkDto> CreateLinksForBooks(BookResourceDto bookResource, bool hasNext, bool hasPrevious)
    {
        var links = new List<LinkDto>();

        //self
        links.Add(new(CreateBookResourceUri(bookResource, ResourceUriType.Current), "self", "GET"));

        if (hasNext)
            links.Add(new(CreateBookResourceUri(bookResource, ResourceUriType.NextPage), "nextPage", "GET"));
        if (hasPrevious)
            links.Add(new(CreateBookResourceUri(bookResource, ResourceUriType.PreviousPage), "previousPage", "GET"));

        return links;
    }

    private string? CreateBookResourceUri(BookResourceDto bookResource, ResourceUriType resourceType)
    {
        switch(resourceType)
        {
            case ResourceUriType.PreviousPage:
                return Url.Link("GetAllBooks",
                    new
                    {
                        pageNumber = bookResource.PageNumber -1,
                        pageSize = bookResource.PageSize
                    });

            case ResourceUriType.NextPage:
                return Url.Link("GetAllBooks",
                    new
                    {
                        pageNumber = bookResource.PageNumber + 1,
                        pageSize = bookResource.PageSize
                    });

            case ResourceUriType.Current:
            default:
                var temp = Url.Link("GetAllBooks",
                    new
                    {
                        pageNumber = bookResource.PageNumber,
                        pageSize = bookResource.PageSize
                    });
                return Url.Link("GetAllBooks",
                    new
                    {
                        pageNumber = bookResource.PageNumber,
                        pageSize = bookResource.PageSize
                    });
        }
    }
    #endregion
}
