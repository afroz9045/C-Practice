using EnrollmentLib.Models;
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
        

            modelBuilder.Seed();
           
        }
    }
}
