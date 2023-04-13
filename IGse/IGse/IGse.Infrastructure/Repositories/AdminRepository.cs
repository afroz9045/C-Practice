using Dapper;
using IGse.Core.Contracts.Repositories;
using IGse.Core.Dtos;
using System.Data;

namespace IGse.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IDbConnection _dbConnection;

        public AdminRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<AdminDto>> GetAdmins()
        {
            var adminsQuery = "Exec [GetAdmins]";
            var admins = await _dbConnection.QueryAsync<AdminDto>(adminsQuery);
            return admins;
        }
    }
}
