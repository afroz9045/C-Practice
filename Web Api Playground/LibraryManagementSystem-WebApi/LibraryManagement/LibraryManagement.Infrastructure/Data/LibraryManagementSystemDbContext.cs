using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Data
{
    public partial class LibraryManagementSystemDbContext : DbContext
    {
        public LibraryManagementSystemDbContext()
        {
        }

        public LibraryManagementSystemDbContext(DbContextOptions<LibraryManagementSystemDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Designation> Designations { get; set; } = null!;
        public virtual DbSet<Issue> Issues { get; set; } = null!;
        public virtual DbSet<Return> Returns { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Staff> staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RegisterEntityConfigurations();
        }
    }
}