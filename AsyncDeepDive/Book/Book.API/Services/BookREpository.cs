using Book.API.DbContexts;
using Book.API.Entities;
using Book.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Book.API.Services;

public class BookREpository : IBookREpository
{
    private readonly BooksDbContext _dbContext;
    public BookREpository(BooksDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Books> GetBooks()
    {
        return _dbContext.Books
                .Include(b=>b.Author)
                .ToList();
    }

    public async Task<PagedList<Books>> GetBooksAsync(BookResourceDto query)
    {
        var collection = _dbContext.Books.AsNoTracking();
        return await PagedList<Books>.CreateAsync(collection, query.PageNumber, query.PageSize);
    }

    public async Task<Books> GetBooksAsync(int id)
    {
        return await  _dbContext.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b=>b.Id == id);
    }
}
