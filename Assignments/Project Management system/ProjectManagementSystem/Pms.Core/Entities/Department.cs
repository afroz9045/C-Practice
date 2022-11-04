namespace Pms.Core.Entities
{
    public class Department
    {
        public int DeptId { get; set; }
        public string? DeptName { get; set; }
        public long PhoneNumber { get; set; }

        public override string? ToString()
        {
            return $"\t{DeptId}\t\t{PhoneNumber}\t\t{DeptName}";
        }


    }
}
