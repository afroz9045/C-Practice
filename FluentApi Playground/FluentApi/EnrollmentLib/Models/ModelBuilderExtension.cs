using Microsoft.EntityFrameworkCore;

namespace EnrollmentLib.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().Property(t => t.FirstMidName).HasColumnName("FirstName");
            modelBuilder.Entity<Course>().Property(c => c.Title).HasColumnName("CourseName");
            //modelBuilder.Entity<Course>().ToTable("Courses", schema: "Course");
            modelBuilder.HasDefaultSchema("Enrollment");
            modelBuilder.Entity<Course>().HasKey(c => new { c.Credits, c.Title });

            modelBuilder.Entity<Student>().HasData(new Student
            {
                ID = 1,
                LastName = "khan",
                FirstMidName = "Shabaz",
                EnrollmentDate = new DateTime()
            });    
            modelBuilder.Entity<Student>().HasData(new Student
            {
                ID = 2,
                LastName = "khan",
                FirstMidName = "Sarfaraz",
                EnrollmentDate = new DateTime()
            });
            
        }
    }
}
