using ClientPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientPro.Infrastructure.DataContext
{
    public class ClientDbContext : DbContext
    {
        public ClientDbContext(DbContextOptions<ClientDbContext> options)
            :base(options)
        {
            
        }

        #region DbSets

        public DbSet<Client> Clients { get; set; }

        #endregion
    }
}
