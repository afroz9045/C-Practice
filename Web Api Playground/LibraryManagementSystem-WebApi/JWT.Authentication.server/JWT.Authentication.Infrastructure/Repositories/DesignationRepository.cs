using JWT.Authentication.Core.Entities;
using JWT.Authentication.Infrastructure.DataContext;
using JWT.Authentication.Server.Core.Contract.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JWT.Authentication.Infrastructure.Repositories
{
    public class DesignationRepository : IDesignationRepository
    {
        private readonly LibraryManagementSystemDbContext _dbContext;

        public DesignationRepository(LibraryManagementSystemDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<string?> GetUserDesignation(string staffId)
        {
            var designation = await (from staff in _dbContext.staff
                                     join credential in _dbContext.Credentials
                                     on staff.StaffId equals credential.StaffId
                                     join desig in _dbContext.Designations
                                     on staff.DesignationId equals desig.DesignationId
                                     where staff.StaffId == staffId
                                     select new Designation
                                     {
                                         DesignationName = desig.DesignationName
                                     }).FirstOrDefaultAsync();
            return designation != null ? designation.DesignationName : null;
        }
    }
}