using Dapper;
using EmployeeRecordBook.Core.Dtos;
using EmployeeRecordBook.Core.Entities;
using EmployeeRecordBook.Core.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;
namespace EmployeeRecordBook.Infrastructure.Repositories.Dapper
{
    public class EmployeeDapperRepository : IEmployeeRepository
   {
      private readonly IDbConnection _dbConnection;
      public EmployeeDapperRepository(IDbConnection connection)
      {
         _dbConnection = connection;
      }
   
    public async Task<IEnumerable<EmployeeMinimumData>> GetEmployeesByView()
        {
            var viewQuery = "Select * from vEmployeeRecord";
            var result = await _dbConnection.QueryAsync<EmployeeMinimumData>(viewQuery);
            return result;

        }
      public async Task<Employee> CreateAsync(Employee employee)
      {
         var command = "Insert Employee(Name, Email, Salary, DepartmentId) Values(@Name, @Email, @Salary, @DepartmentId)";
         var result = await _dbConnection.ExecuteAsync(command, employee);
         return employee;
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
