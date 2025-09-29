using Microsoft.EntityFrameworkCore;

namespace product_service.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

    }
}
