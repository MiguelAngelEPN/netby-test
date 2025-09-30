using Microsoft.EntityFrameworkCore;
using transaccion_service.Models;

namespace transaccion_service.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Producto> Producto { get; set; } = null!;
        public DbSet<Transaccion> Transaccion { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaccion>()
                .Property(t => t.TipoTransaccion)
                .HasConversion<string>();  // conviertir enum a su valor string
            base.OnModelCreating(modelBuilder);
        }
    }
}
