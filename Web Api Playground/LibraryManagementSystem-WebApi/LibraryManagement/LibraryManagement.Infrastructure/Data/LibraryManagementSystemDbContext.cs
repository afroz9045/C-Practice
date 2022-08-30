using LibraryManagement.Core.Entities;
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
        public virtual DbSet<staff> staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localDb)\\MSSQLLocalDB;Database=LibraryManagementSystemDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("books");

                entity.Property(e => e.BookId).HasColumnName("bookId");

                entity.Property(e => e.AuthorName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("authorName");

                entity.Property(e => e.BookEdition)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("bookEdition");

                entity.Property(e => e.BookName)
                    .HasMaxLength(15)
                    .HasColumnName("bookName");

                entity.Property(e => e.Isbn).HasColumnName("isbn");

                entity.Property(e => e.StockAvailable).HasColumnName("stockAvailable");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DeptId)
                    .HasName("PK__departme__BE2D26B6D0D236DC");

                entity.ToTable("department");

                entity.Property(e => e.DeptId).HasColumnName("deptId");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(50)
                    .HasColumnName("departmentName");
            });

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.ToTable("designation");

                entity.Property(e => e.DesignationId)
                    .HasMaxLength(50)
                    .HasColumnName("designationId");

                entity.Property(e => e.Designation1)
                    .HasMaxLength(20)
                    .HasColumnName("designation");

                entity.Property(e => e.StaffId)
                    .HasMaxLength(50)
                    .HasColumnName("staffId");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Designations)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__designati__staff__46E78A0C");
            });

            modelBuilder.Entity<Issue>(entity =>
            {
                entity.ToTable("issue");

                entity.Property(e => e.IssueId).HasColumnName("issueId");

                entity.Property(e => e.BookId).HasColumnName("bookId");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnType("date")
                    .HasColumnName("expiryDate");

                entity.Property(e => e.IssueDate)
                    .HasColumnType("date")
                    .HasColumnName("issueDate");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Issues)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__issue__bookId__4222D4EF");
            });

            modelBuilder.Entity<Return>(entity =>
            {
                entity.ToTable("return");

                entity.Property(e => e.ReturnId).HasColumnName("returnId");

                entity.Property(e => e.BookId).HasColumnName("bookId");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnType("date")
                    .HasColumnName("expiryDate");

                entity.Property(e => e.IssueDate)
                    .HasColumnType("date")
                    .HasColumnName("issueDate");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Returns)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__return__bookId__38996AB5");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.Property(e => e.StudentId).HasColumnName("studentId");

                entity.Property(e => e.DepartmentId).HasColumnName("departmentId");

                entity.Property(e => e.Gender)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.StudentDepartment)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("studentDepartment");

                entity.Property(e => e.StudentName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("studentName");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__student__departm__3F466844");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.Property(e => e.StaffId)
                    .HasMaxLength(50)
                    .HasColumnName("staffId");

                entity.Property(e => e.Designation)
                    .HasMaxLength(50)
                    .HasColumnName("designation");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .HasColumnName("gender");

                entity.Property(e => e.StaffName)
                    .HasMaxLength(20)
                    .HasColumnName("staffName");

                entity.HasOne(d => d.DesignationNavigation)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.Designation)
                    .HasConstraintName("FK__staff__designati__47DBAE45");
            });

            //OnModelCreatingPartial(modelBuilder);
        }

        //private partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}