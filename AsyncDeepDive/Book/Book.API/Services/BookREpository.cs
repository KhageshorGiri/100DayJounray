using Book.API.DbContexts;
using Book.API.Entities;
using Book.API.Helpers;
using Book.API.Models;
using Book.Legcay;
using Microsoft.EntityFrameworkCore;

namespace Book.API.Services;

public class BookREpository : IBookREpository
{
    private readonly BooksDbContext _dbContext;
    private readonly IPropertyMappingService _propertyMappingService;
    private readonly ILogger<BookREpository> _logger;
    public BookREpository(BooksDbContext dbContext, IPropertyMappingService propertyMappingService, ILogger<BookREpository> logger)
    {
        _dbContext = dbContext;
        _propertyMappingService = propertyMappingService;
        _logger = logger;
    }

    public IEnumerable<Books> GetBooks()
    {
        return _dbContext.Books
                .Include(b => b.Author)
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

    public async Task<Books> GetBooksAsync(int id, CancellationToken cancellationToken)
    {
        var books = new Books();
        books = await _dbContext.Books
             .Include(b => b.Author)
             .FirstOrDefaultAsync(b => b.Id == id, cancellationToken: cancellationToken);

        return books;

    }

    public async Task<Books> GetBooksAsync_BadCode(int id)
    {
        _logger.LogInformation($"Before Running Task Thread Id : {Thread.CurrentThread.ManagedThreadId}");
        var books = new Books();
        await FindBook(id);
        books = await _dbContext.Books
             .Include(b => b.Author)
             .FirstOrDefaultAsync(b => b.Id == id);

        return books;
    }

    private Task<int> FindBook(int id)
    {
        return Task.Run(() =>
        {
            _logger.LogInformation($"After Running Task Thread Id : {Thread.CurrentThread.ManagedThreadId}");
            var findOldBooks = new SearchBooks();
            return findOldBooks.FindBookById(id);
        });
    }
}
