namespace Pms.Core.Entities
{
    public class Assignment
    {
        public int ProjectId { get; set; }
        public int EmployeeNumber { get; set; }
        public int HoursWorked { get; set; }
        public string? AssignmentName { get; set; }
       

        public override string? ToString()
        {
            return $"{ProjectId}\t\t{EmployeeNumber}\t\t{HoursWorked}";
        }

    }
}
