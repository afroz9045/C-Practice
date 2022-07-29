

namespace Pms.Core.Entities
{
    public class Department
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public long phoneNumber { get; set; }

        public override string? ToString()
        {
            return $"{DeptId}\t{phoneNumber}\t{DeptName}";
        }


    }
}
