using Book.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Book.API.DbContexts;

public class BooksDbContext : DbContext
{
    public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options)
    {
    }

    public DbSet<Books> Books { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
