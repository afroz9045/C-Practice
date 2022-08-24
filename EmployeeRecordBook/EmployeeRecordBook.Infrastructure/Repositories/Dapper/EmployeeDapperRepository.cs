using Dapper;
using EmployeeRecordBook.Core.Dtos;
using EmployeeRecordBook.Core.Entities;
using System.Data;

namespace EmployeeRecordBook.Infrastructure.Repositories.Dapper
{
    public class EmployeeDapperRepository : IEmployeeRepository
   {
      private readonly IDbConnection _dbConnection;
      public EmployeeDapperRepository(IDbConnection dbConnection)
      {
         _dbConnection = dbConnection;
      }

        public async Task<Employee> CreateAsync(Employee employee)
      {
         var command = "Insert Employee(Name, Email, Salary, DepartmentId) Values(@Name, @Email, @Salary, @DepartmentId)";
         var result = await _dbConnection.ExecuteAsync(command, employee);
         return employee;
      }

        public Task CreateRangeAsync(IEnumerable<Employee> employees)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<EmployeeDetailsByView>> GetEmployeeDetailsByView()
        {
            var viewQuery = "select * from vEmployeeRecord";
            return await _dbConnection.QueryAsync<EmployeeDetailsByView>(viewQuery);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeeByProcedure()
        {
            var procedureQuery = "exec spGetEmployees";
            return await _dbConnection.QueryAsync<EmployeeDto>(procedureQuery);
        }
        public async Task<IEnumerable<EmployeeDto>> GetEmployeeByIdProcedure()
        {
            var procedureQuery = "exec spGetEmployeesById 3";
            return await _dbConnection.QueryAsync<EmployeeDto>(procedureQuery);
        }
        public async Task DeleteAsync(int employeeId)
      {
         var command = "Delete from Employee where Id = @Id";
         await _dbConnection.ExecuteAsync(command, new { Id = employeeId });
      }

      public async Task<Employee> GetEmployeeAsync(int employeeId)
      {
         var query = "Select * from Employee where Id = @employeeId";  // Recomendation: Use same name as the DB field name. 
         return (await _dbConnection.QueryAsync<Employee>(query, new {employeeId})).FirstOrDefault();
      }

      public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
      {
         var query = "SELECT [e].[Id], [e].[Name], [e].[Salary], [d].[Name] AS [DepartmentName] FROM[Employee] AS[e] INNER JOIN[Department] AS[d] ON[e].[DepartmentId] = [d].[Id]";
         return await _dbConnection.QueryAsync<EmployeeDto>(query);
      }

        public Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(int pageIndex, int pageSize, string sortOrder, string sortField, string filterText = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> UpdateAsync(int employeeId, Employee employee)
      {
         employee.Id = employeeId;
         var command = "Update Employee Set Name = @Name, Salary = @Salary Where Id = @Id";
         //await _dbConnection.ExecuteAsync(command, new {Id = employeeId, Name = employee.Name, Salary = employee.Salary});
         await _dbConnection.ExecuteAsync(command, employee);
         return employee;
      }

       
    }
}
