﻿using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.EntityConfigurations
{
    internal class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("student");

            builder.Property(e => e.StudentId).HasColumnName("StudentId");

            builder.Property(e => e.DepartmentId).HasColumnName("DepartmentId");

            builder.Property(e => e.Gender)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Gender");

            builder.Property(e => e.StudentDepartment)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("StudentDepartment");

            builder.Property(e => e.StudentName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("StudentName");

            builder.HasOne(d => d.Department)
                .WithMany(p => p.Students)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__student__departm__3F466844");
        }
    }
}