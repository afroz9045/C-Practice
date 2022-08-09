using Microsoft.EntityFrameworkCore;

namespace EnrollmentLib.DataContext
{
    public class EnrollmentContext:DbContext
    {
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected  override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = (localDb)\MSSQLLocalDB; Database = EnrollmentDb; Trusted_Connection = True; ");
        }
        protected  override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().Property(t => t.FirstMidName).HasColumnName("FirstName");
            modelBuilder.Entity<Course>().Property(c => c.Title).HasColumnName("CourseName");
            //modelBuilder.Entity<Course>().ToTable("Courses", schema: "Course");
            modelBuilder.HasDefaultSchema("Enrollment");
            modelBuilder.Entity<Course>().HasKey(c => new { c.Credits, c.Title });
        }
    }
}
