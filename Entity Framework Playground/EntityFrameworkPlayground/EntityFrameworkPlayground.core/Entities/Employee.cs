using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkPlayground.core.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }

        public Department Department { get; set; }
    }
}
