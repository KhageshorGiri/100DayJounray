using Book.API.Entities;
using Book.API.Models;

namespace Book.API.Services;

public interface IBookREpository
{
    IEnumerable<Books> GetBooks();
    Task<PagedList<Books>> GetBooksAsync(BookResourceDto query);
    Task<Books> GetBooksAsync(int id, CancellationToken cancellationToken);
    Task<Books> GetBooksAsync_BadCode(int id);
}
