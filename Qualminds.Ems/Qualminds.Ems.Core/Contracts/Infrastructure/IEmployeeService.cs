using Qualminds.Ems.Core.Entities;
using System.Text;

namespace Qualminds.Ems.Core.Contracts.Infrastructure
{
   public interface IEmployeeService
   {
      public bool InitializeEmployeeService();

      public Employee AddEmployee(Employee employee);

      public IEnumerable<Employee> AddEmployees(IEnumerable<Employee> employees);

      public StringBuilder GetEmployees();

      public void DeleteEmployees();
   }
}
