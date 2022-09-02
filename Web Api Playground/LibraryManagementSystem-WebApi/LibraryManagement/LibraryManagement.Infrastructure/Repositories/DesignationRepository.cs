using Dapper;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Core.Contracts;
using System.Data;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class DesignationRepository : IDesignationRepository
    {
        private readonly LibraryManagementSystemDbContext _libraryDbContext;
        private readonly IDbConnection _dapperConnection;

        public DesignationRepository(LibraryManagementSystemDbContext libraryDbContext, IDbConnection dapperConnection)
        {
            _libraryDbContext = libraryDbContext;
            _dapperConnection = dapperConnection;
        }

        public async Task<IEnumerable<Designation>> GetDesignationAsync()
        {
            var getDesignationQuery = "select * from [designation]";
            var departmentData = await _dapperConnection.QueryAsync<Designation>(getDesignationQuery);
            return departmentData;
        }

        public async Task<Designation> GetDesignationByIdAsync(string designationId)
        {
            var getDesignationByIdQuery = "select * from [designation] where designationId = @designationId";
            return (await _dapperConnection.QueryFirstAsync<Designation>(getDesignationByIdQuery, new { designationId = designationId }));
        }

        public async Task<Designation> AddDesignationAsync(Designation designation)
        {
            var designationRecord = new Designation()
            {
                DesignationId = designation.DesignationId,
                DesignationName = designation.DesignationName
            };
            _libraryDbContext.Designations.Add(designationRecord);
            await _libraryDbContext.SaveChangesAsync();
            return designationRecord;
        }

        public async Task<Designation> UpdateDesignationAsync(string designationId, Designation designation)
        {
            var designationRecord = await GetDesignationByIdAsync(designationId);

            designationRecord.DesignationId = designationId;
            designationRecord.DesignationName = designation.DesignationName;

            _libraryDbContext.Update(designationRecord);
            await _libraryDbContext.SaveChangesAsync();
            return designationRecord;
        }

        public async Task<Designation> DeleteDepartmentAsync(string designationId)
        {
            var designationRecord = await GetDesignationByIdAsync(designationId);
            _libraryDbContext.Designations?.Remove(designationRecord);
            await _libraryDbContext.SaveChangesAsync();
            return designationRecord;
        }
    }
}