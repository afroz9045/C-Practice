using System.Text;
namespace Pms.Core.Entities
{
    public class Assignment
    {
        public int ProjectId { get; set; }
        public int EmployeeNumber { get; set; }
        public int HoursWorked { get; set; }
        StringBuilder sb=new StringBuilder();
        
        public override string? ToString()
        {
            return $"{ProjectId}\t\t{EmployeeNumber}\t\t{HoursWorked}";
        }

    }
}
