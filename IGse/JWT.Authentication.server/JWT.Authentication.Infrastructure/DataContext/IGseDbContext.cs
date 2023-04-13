using JWT.Authentication.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JWT.Authentication.Infrastructure.DataContext
{
    public partial class IGseDbContext : DbContext
    {
        public IGseDbContext()
        {
        }

        public IGseDbContext(DbContextOptions<IGseDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customers> Customers { get; set; } = null!;
        public virtual DbSet<Users> Users { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<Customers>().ToTable("Customer");
            modelBuilder.Entity<Users>().ToTable("Users");
        }
    }
}