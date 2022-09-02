using Dapper;
using LibraryManagement.Core.Contracts;
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
            var staffRecord = new Staff()
            {
                StaffId = staff.StaffId,
                StaffName = staff.StaffName,
                Gender = staff.Gender,
                DesignationId = staff.DesignationId
            };
            _libraryDbContext.Add(staffRecord);
            await _libraryDbContext.SaveChangesAsync();
            return staffRecord;
        }

        public async Task<IEnumerable<Staff>> GetStaffAsync()
        {
            var getStaffQuery = "select * from [staff]";
            var result = await _dapperConnection.QueryAsync<Staff>(getStaffQuery);
            return result;
        }

        public async Task<Staff> GetStaffByIDAsync(string staffId)
        {
            var getStaffByIdQuery = "select * from [staff] where staffId = @staffId";
            return (await _dapperConnection.QueryFirstAsync<Staff>(getStaffByIdQuery, new { staffId = staffId }));
        }

        public async Task<Staff> UpdateStaffAsync(Staff staff, string staffId)
        {
            var staffRecord = await GetStaffByIDAsync(staffId);
            staffRecord.StaffId = staffId;
            staffRecord.StaffName = staff.StaffName;
            staffRecord.Gender = staff.Gender;
            staffRecord.DesignationId = staff.DesignationId;

            _libraryDbContext.Update(staffRecord);
            await _libraryDbContext.SaveChangesAsync();
            return staffRecord;
        }

        public async Task<Staff> DeleteStaffAsync(string staffId)
        {
            var staffRecord = await GetStaffByIDAsync(staffId);
            _libraryDbContext.Remove(staffRecord);
            await _libraryDbContext.SaveChangesAsync();
            return staffRecord;
        }
    }
}