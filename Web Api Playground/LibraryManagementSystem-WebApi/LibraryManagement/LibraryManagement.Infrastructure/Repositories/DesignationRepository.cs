using Dapper;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Data;
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

        public async Task<IEnumerable<Designation>?> GetDesignationAsync()
        {
            var getDesignationQuery = "select * from [designation]";
            var designationData = await _dapperConnection.QueryAsync<Designation>(getDesignationQuery);
            return designationData;
        }

        public async Task<Designation?> GetDesignationByIdAsync(string designationId)
        {
            var getDesignationByIdQuery = "select * from [designation] where DesignationId = @designationId";
            var designation = await _dapperConnection.QueryFirstOrDefaultAsync<Designation>(getDesignationByIdQuery, new { designationId });
            return designation;
        }

        public async Task<Designation?> GetDesignationByNameAsync(string designationName)
        {
            var getDesignationByIdQuery = "select * from [designation] where DesignationName = @designationName";
            var designation = await _dapperConnection.QueryFirstOrDefaultAsync<Designation>(getDesignationByIdQuery, new { designationName });
            return designation;
        }

        public async Task<Designation?> AddDesignationAsync(Designation designation)
        {
            var designationAdded = _libraryDbContext.Designations.Add(designation);
            await _libraryDbContext.SaveChangesAsync();
            return designation;
        }

        public async Task<Designation?> GetRecentInsertedDesignation()
        {
            var recentQuery = "SELECT TOP 1 * FROM designation ORDER BY DesignationId DESC";
            var designationData = await _dapperConnection.QueryFirstOrDefaultAsync<Designation>(recentQuery);
            return designationData;
        }

        public async Task<Designation?> UpdateDesignationAsync(Designation designation)
        {
            var updatedDesignation = _libraryDbContext.Update(designation);
            await _libraryDbContext.SaveChangesAsync();
            return designation;
        }

        public async Task<Designation?> DeleteDesignationAsync(Designation designation)
        {
            var deletedDesignation = _libraryDbContext.Designations?.Remove(designation);
            await _libraryDbContext.SaveChangesAsync();
            return designation;
        }
    }
}