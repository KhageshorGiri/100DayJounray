using Book.API.Entities;
using Book.API.Models;

namespace Book.API.Services;

public interface IBookREpository
{
    IEnumerable<Books> GetBooks();
    Task<IEnumerable<Books>> GetBooksAsync(PaginationQuery query);
    Task<Books> GetBooksAsync(int id);
}
