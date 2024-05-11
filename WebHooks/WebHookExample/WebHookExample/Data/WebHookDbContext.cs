using Microsoft.EntityFrameworkCore;
using WebHookExample.Models;

namespace WebHookExample.Data
{
    public class WebHookDbContext : DbContext
    {
        public WebHookDbContext(DbContextOptions<WebHookDbContext> options)
          : base(options)
        {
        }

        // Debsets

        public DbSet<Event> Events { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

      
    }
}
