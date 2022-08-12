using System;
using System.Collections.Generic;
using EmployeeRecordBook.Core.Entities;
using EmployeeRecordBook.Infrastructure.EntityConfigurations;
using EmployeeRecordBook.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EmployeeRecordBook.Infrastructure.Data
{
   public partial class EmployeeContext : DbContext
   {
      public EmployeeContext()
      {
      }

      public EmployeeContext(DbContextOptions<EmployeeContext> options)
          : base(options)
      {
      }

      public virtual DbSet<Department> Departments { get; set; } = null!;
      public virtual DbSet<Employee> Employees { get; set; } = null!;

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         if (!optionsBuilder.IsConfigured)
         {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            optionsBuilder.UseSqlServer(@"Server=(localDb)\MSSQLLocalDB;Database = EmployeeRecordBook;Trusted_Connection = True;");
         }
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.RegisterEntityConfigurations();

         OnModelCreatingPartial(modelBuilder);
      }

      partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
   }
}
