using IGse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace IGse.Infrastructure.Data
{
    public partial class GseDbContext : DbContext
    {
        public GseDbContext()
        {
        }

        public GseDbContext(DbContextOptions<GseDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<Customers> Customers { get; set; } = null!;
        public virtual DbSet<Evc> Evcs { get; set; } = null!;
        public virtual DbSet<Payments> Payments { get; set; } = null!;
        public virtual DbSet<SetPrice> SetPrices { get; set; } = null!;
        public virtual DbSet<SetPriceHistory> SetPriceHistory { get; set; } = null!;
        public virtual DbSet<CustomerEvcHistory> CustomerEvcHistory { get; set; } = null!;
        public virtual DbSet<Users> Users { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<Bill>().ToTable("Bill");
            modelBuilder.Entity<Customers>().ToTable("Customer");
            modelBuilder.Entity<Evc>().ToTable("Evc");
            modelBuilder.Entity<Payments>().ToTable("Payments").HasKey(x=>x.CustomerId);
            modelBuilder.Entity<Payments>().ToTable("Payments").HasKey(x=>x.BillId);
            modelBuilder.Entity<SetPrice>().ToTable("SetPrice");
            modelBuilder.Entity<SetPriceHistory>().ToTable("SetPriceHistory");
            modelBuilder.Entity<CustomerEvcHistory>().ToTable("CustomerEvcHistory");
            modelBuilder.Entity<Users>().ToTable("User");
        }
    }
}