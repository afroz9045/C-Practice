using JWT.Authentication.Core.Entities;
using JWT.Authentication.Infrastructure.DataContext;
using JWT.Authentication.Server.Core.Contract.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JWT.Authentication.Infrastructure.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly LibraryManagementSystemDbContext _dbContext;

        public StaffRepository(LibraryManagementSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Staff?> GetStaffByStaffId(string staffId)
        {
            var staffDetails = await (from staff in _dbContext.staff
                                      where staff.StaffId == staffId
                                      select staff).FirstOrDefaultAsync();
            return staffDetails;
        }

       
    }
}