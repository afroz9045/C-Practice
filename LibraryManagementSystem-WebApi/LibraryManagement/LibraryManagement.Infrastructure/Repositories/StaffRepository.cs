using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IDbConnection _dapperConnection;

        public StaffRepository(LibraryManagementSystemDbContext libraryDbContext, IMapper mapper, IDbConnection dapperConnection)
        {
            _libraryDbContext = libraryDbContext;
            _mapper = mapper;
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

        public async Task<Staff?> GetRecentInsertedStaff()
        {
            var recentQuery = "SELECT TOP 1 * FROM staff ORDER BY StaffId DESC";
            var staffData = await _dapperConnection.QueryFirstOrDefaultAsync<Staff>(recentQuery);
            return staffData;
        }

        public async Task<Staff?> GetStaffByIdAsync(string? staffId)
        {
            var getStaffByIdQuery = "select * from [staff] where staffId = @staffId";
            return (await _dapperConnection.QueryFirstOrDefaultAsync<Staff>(getStaffByIdQuery, new { staffId }));
        }

        public async Task<Staff?> GetStaffByName(string staffName)
        {
            var getStaffByNameQuery = "select * from [staff] where StaffName = @staffName";
            return (await _dapperConnection.QueryFirstOrDefaultAsync<Staff>(getStaffByNameQuery, new { staffName }));
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