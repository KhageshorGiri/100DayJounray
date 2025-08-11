using Book.API.DbContexts;
using Book.API.Entities;
using Book.API.Helpers;
using Book.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Book.API.Services;

public class BookREpository : IBookREpository
{
    private readonly BooksDbContext _dbContext;
    private readonly IPropertyMappingService _propertyMappingService;
    public BookREpository(BooksDbContext dbContext, IPropertyMappingService propertyMappingService)
    {
        _dbContext = dbContext;
        _propertyMappingService = propertyMappingService;
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

        if (!string.IsNullOrEmpty(query.OrderBy))
        {
            // get property mapping dictionary
            var bookPropertyMappingDictionary = _propertyMappingService.GetPropertyMapping<BookDto, Books>();
            collection = collection.ApplySort(query.OrderBy, bookPropertyMappingDictionary);
        }

        return await PagedList<Books>.CreateAsync(collection, query.PageNumber, query.PageSize);
    }

    public async Task<Books> GetBooksAsync(int id)
    {
        return await  _dbContext.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b=>b.Id == id);
    }
}
