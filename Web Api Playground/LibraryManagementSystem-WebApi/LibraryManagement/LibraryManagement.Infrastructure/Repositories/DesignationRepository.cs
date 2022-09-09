using Dapper;
using LibraryManagement.Core.Contracts;
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

        public async Task<IEnumerable<Designation>> GetDesignationAsync()
        {
            var getDesignationQuery = "select * from [designation]";
            var designationData = await _dapperConnection.QueryAsync<Designation>(getDesignationQuery);
            return designationData;
        }

        public async Task<Designation> GetDesignationByIdAsync(string designationId)
        {
            var getDesignationByIdQuery = "select * from [designation] where designationId = @designationId";
            return (await _dapperConnection.QueryFirstAsync<Designation>(getDesignationByIdQuery, new { designationId = designationId }));
        }

        public async Task<Designation> AddDesignationAsync(Designation designation)
        {
            var designationId = await GenerateDesignationId();
            var designationRecord = new Designation()
            {
                DesignationId = designationId,
                DesignationName = designation.DesignationName
            };
            _libraryDbContext.Designations.Add(designationRecord);
            await _libraryDbContext.SaveChangesAsync();
            return designationRecord;
        }

        public async Task<Designation?> GetRecentInsertedDesignation()
        {
            var recentQuery = "SELECT TOP 1 * FROM designation ORDER BY DesignationId DESC";
            var designationData = await _dapperConnection.QueryFirstOrDefaultAsync<Designation>(recentQuery);
            if (designationData != null)
                return designationData;
            return null;
        }

        public async Task<string?> GenerateDesignationId()
        {
            var recentDesignationRecord = await GetRecentInsertedDesignation();
            if (recentDesignationRecord != null && recentDesignationRecord.DesignationId != null)
            {
                var firstCharacter = recentDesignationRecord.DesignationId.Substring(0, 1);
                var remainingNumber = Convert.ToInt32(recentDesignationRecord.DesignationId.Substring(1));
                var resultantDesignationId = Convert.ToString(firstCharacter + (remainingNumber + 1));
                return resultantDesignationId;
            }
            return "A100";
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

        public async Task<Designation> DeleteDesignationAsync(string designationId)
        {
            var designationRecord = await GetDesignationByIdAsync(designationId);
            _libraryDbContext.Designations?.Remove(designationRecord);
            await _libraryDbContext.SaveChangesAsync();
            return designationRecord;
        }
    }
}