using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace LibraryManagement.Infrastructure.EntityConfigurations
{
    internal class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("student");

            builder.Property(e => e.StudentId).HasColumnName("studentId");

            builder.Property(e => e.DepartmentId).HasColumnName("departmentId");

            builder.Property(e => e.Gender)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("gender");

            builder.Property(e => e.StudentDepartment)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("studentDepartment");

            builder.Property(e => e.StudentName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("studentName");

            builder.HasOne(d => d.Department)
                .WithMany(p => p.Students)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__student__departm__3F466844");
        }
    }
}