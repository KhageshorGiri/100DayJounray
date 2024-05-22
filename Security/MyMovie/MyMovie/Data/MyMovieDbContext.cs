using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMovie.Models;

namespace MyMovie.Data
{
    public class MyMovieDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyMovieDbContext(DbContextOptions<MyMovieDbContext> options)
            :base(options)
        {
            
        }

        #region Dbset
        public DbSet<TokenInfo> TokenInfo { get; set; }
        #endregion
    }

}
