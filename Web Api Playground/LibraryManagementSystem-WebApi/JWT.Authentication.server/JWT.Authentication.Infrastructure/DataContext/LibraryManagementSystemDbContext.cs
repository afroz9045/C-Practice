﻿using JWT.Authentication.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JWT.Authentication.Infrastructure.DataContext
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

        public virtual DbSet<Credential> Credentials { get; set; } = null!;
        public virtual DbSet<staff> staff { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Credential>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SaltedPassword)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).
                UseIdentityColumn()
                    .ValueGeneratedOnAdd()
                    .HasColumnName("UserID");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.Property(e => e.StaffId).HasMaxLength(50);

                entity.Property(e => e.DesignationId).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.StaffName).HasMaxLength(20);
            });

            //OnModelCreatingPartial(modelBuilder);
        }

        //private partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}