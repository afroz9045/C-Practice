using Dapper;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Data;
using System.Data;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly LibraryManagementSystemDbContext _libraryDbContext;
        private readonly IDbConnection _dapperConnection;

        public StaffRepository(LibraryManagementSystemDbContext libraryDbContext, IDbConnection dapperConnection)
        {
            _libraryDbContext = libraryDbContext;
            _dapperConnection = dapperConnection;
        }

        public async Task<Staff> AddStaffAsync(Staff staff)
        {
            _libraryDbContext.Add(staff);
            await _libraryDbContext.SaveChangesAsync();
            return staff;
        }

        public async Task<IEnumerable<Staff>> GetStaffAsync()
        {
            var getStaffQuery = "select * from [staff]";
            var result = await _dapperConnection.QueryAsync<Staff>(getStaffQuery);
            return result;
        }

        public async Task<Staff?> GetStaffByIdAsync(string staffId)
        {
            if (staffId != null)
            {
                var getStaffByIdQuery = "select * from [staff] where staffId = @staffId";
                return (await _dapperConnection.QueryFirstOrDefaultAsync<Staff>(getStaffByIdQuery, new { staffId = staffId }));
            }
            return null;
        }

        public async Task<Staff> UpdateStaffAsync(Staff staff)
        {
            _libraryDbContext.Update(staff);
            await _libraryDbContext.SaveChangesAsync();
            return staff;
        }

        public async Task<Staff> DeleteStaffAsync(Staff staff)
        {
            _libraryDbContext.Remove(staff);
            await _libraryDbContext.SaveChangesAsync();
            return staff;
        }
    }
}