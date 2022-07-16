using Qualminds.Ems.Core.Constants;
using Qualminds.Ems.Core.Contracts.Infrastructure;
using Qualminds.Ems.Core.Entities;

namespace Qualminds.Ems.Infrastructure.IO
{
   public class EmployeeService : IEmployeeService
   {
      
      private readonly string _filePath;
        private Employee _EmpData;
      private string _name;

        public EmployeeService(string filePath)
        {
            _filePath = filePath;
            InitializeEmployeeService();
        }
      public bool InitializeEmployeeService()
      {
         if (!File.Exists(_filePath))
         {
            StreamWriter? streamWriter = null;
            try
            {
               streamWriter = File.CreateText(_filePath);
               streamWriter.WriteLine($"\t\t\t\t\t{FileConstants.EmployeeIdField}{FileConstants.Delimeter}\t\t\t\t\t{FileConstants.EmployeeNameField}{FileConstants.Delimeter}\t\t{FileConstants.EmployeeDesignationField}");
            }
            catch (Exception ex)
            {
               // Log.Error(ex, ex.Message);
               throw;
            }
            finally
            {
               streamWriter?.Flush();
               streamWriter?.Close();
            }
            return true;
         }
         return false;
      }
      public Employee AddEmployee(Employee employee)
      {
         employee.Id = Guid.NewGuid();
         File.AppendAllText(_filePath, $"{employee.Id}{FileConstants.Delimeter}\t\t\t{employee.Name}{FileConstants.Delimeter}\t\t\t{employee.Designation}\n");
         return employee;
      }

      public IEnumerable<Employee> AddEmployees(IEnumerable<Employee> employees)
      {
         throw new NotImplementedException();
      }

      public IEnumerable<Employee> GetEmployees()
      {
         var employeesCommaSeparatedList = File.ReadAllLines(_filePath).Skip(1);
         //var employees = new List<Employee>();   // Replaced with yield return.
         foreach (var employeeRow in employeesCommaSeparatedList)
         {
            var employeeData = employeeRow.Split(FileConstants.Delimeter);
            yield return new Employee { Id = Guid.Parse(employeeData[0]), Name = employeeData[1], Designation = employeeData[2] };
            //employees.Add(employee);
         }
         //return employees;
      }

      public void DeleteEmployees()
      {
         File.Delete(_filePath);
      }


   }
}
