namespace Pms.Core.Entities
{
    public class Employee
    {
        public int EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }

        public override string? ToString()
        {

            return $"{EmployeeNumber}\t{DepartmentId}\t{Phone}\t{Email}\t\t{Salary}\t\t{FirstName}\t\t{LastName}";
        }

    }
}
