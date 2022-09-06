using System;
using System.Collections.Generic;
using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public virtual DbSet<Penalty> Penalties { get; set; } = null!;
        public virtual DbSet<Return> Returns { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Staff> staff { get; set; } = null!;

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("Server=(localDb)\\MSSQLLocalDB;Database=LibraryManagementSystemDb;Trusted_Connection=True;");
        //            }
        //        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Department>(entity =>
        //    {
        //    });

        //    modelBuilder.Entity<Designation>(entity =>
        //    {
        //    });

        //    modelBuilder.Entity<Issue>(entity =>
        //    {
        //    });

        //    modelBuilder.Entity<Penalty>(entity =>
        //    {
        //    });

        //    modelBuilder.Entity<Return>(entity =>
        //    {
        //    });

        //    modelBuilder.Entity<Student>(entity =>
        //    {
        //    });

        //    modelBuilder.Entity<staff>(entity =>
        //    {
        //    });

        //    OnModelCreatingPartial(modelBuilder);
        //}

        //private partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}