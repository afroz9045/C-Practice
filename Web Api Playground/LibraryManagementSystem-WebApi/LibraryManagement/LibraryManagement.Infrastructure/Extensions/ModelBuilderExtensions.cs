using LibraryManagement.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Extensions
{
    internal static class ModelBuilderExtensions
    {
        internal static void RegisterEntityConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DesignationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new IssueEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReturnEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StaffEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StudentEntityTypeConfiguration());
        }
    }
}