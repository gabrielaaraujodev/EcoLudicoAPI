using Microsoft.EntityFrameworkCore;

namespace EcoLudicoAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {}
    }
}
