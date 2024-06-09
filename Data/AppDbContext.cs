using Microsoft.EntityFrameworkCore;
using MvcUsers.Models;

namespace MvcUsers.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts)
        {
            
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
    }
}
