using Book.API.DbContexts;
using Book.API.Entities;

namespace Book.API;

public static class DbSeed
{
    public static void AddSeedData(this WebApplication app)
    {
       using(var scope = app.Services.CreateScope())
       {
            var context = scope.ServiceProvider.GetRequiredService<BooksDbContext>();
            context.Database.EnsureCreated();

            var authorsList = context.Authors.ToList();
            var booksList = context.Books.ToList();

            if (!context.Authors.Any())
            {
                var authors = AddAuthor();
                context.Authors.AddRange(authors);
                context.SaveChanges();

                if (!context.Books.Any())
                {
                    var books = AddBooks(authors);
                    context.Books.AddRange(books);
                    context.SaveChanges();
                }
            }
       }
    }

    private static List<Author> AddAuthor()
    {
        List<Author> authors = new List<Author>();
        for (int i = 0; i < 10; i++)
        {
            authors.Add(new Author {Name = $"Author Name + {i}" });
        }
        return authors;
    }

    private static List<Books> AddBooks(List<Author> author)
    {
        List<Books> books = new List<Books>();
        for (int i = 0; i < 100; i++)
        {
            var authorIds = author.Select(a => a.Id).ToList();
            books.Add(new Books { Title = $"Book Title + {i}", AuthorId = authorIds[i%10], Description = $"Test Data for book Book Title + {i}" });
        }
        return books;
    }
}
