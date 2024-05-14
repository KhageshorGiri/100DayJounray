using Microsoft.EntityFrameworkCore;
using TODOgRpcService.Models;

namespace TODOgRpcService.Data
{
    public class gRpcDbcontext : DbContext
    {
        public gRpcDbcontext(DbContextOptions<gRpcDbcontext> options)
            :base(options)
        {
            
        }

        // Dbset

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
