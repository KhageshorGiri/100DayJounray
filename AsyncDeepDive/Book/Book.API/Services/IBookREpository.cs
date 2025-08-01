using Book.API.Entities;

namespace Book.API.Services;

public interface IBookREpository
{
    IEnumerable<Books> GetBooks();
    Task<IEnumerable<Books>> GetBooksAsync();
    Task<Books> GetBooksAsync(int id);
}
