using EcoLudicoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoLudicoAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {}

        public DbSet<User> Users { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<FavoriteProject> FavoriteProjects { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<School>()
                .HasMany(s => s.Teachers)
                .WithOne(u => u.School)
                .HasForeignKey(u => u.SchoolId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.FavoriteSchools)
                .WithMany(s => s.UsersWhoFavorited)
                .UsingEntity<Dictionary<string, object>>(
                    "UserFavoriteSchools", 
                    j => j.HasOne<School>().WithMany().HasForeignKey("SchoolId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId")
                );
        }
    }
}
