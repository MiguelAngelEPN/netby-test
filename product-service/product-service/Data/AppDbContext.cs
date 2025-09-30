using Microsoft.EntityFrameworkCore;
using product_service.Models;

namespace product_service.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Producto> Producto { get; set; } = null!;
        public DbSet<Transaccion> Transaccion { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
