using Microsoft.EntityFrameworkCore;
using ProductPro.Domain.Models;

namespace ProductProd.Infrastructure.Data
{
    public class ProductProDbContext : DbContext
    {
        public ProductProDbContext(DbContextOptions<ProductProDbContext> options)
            :base(options)
        {
            
        }

        #region dbsets

        public DbSet<Product> Products { get; set; }

        #endregion
    }
}
