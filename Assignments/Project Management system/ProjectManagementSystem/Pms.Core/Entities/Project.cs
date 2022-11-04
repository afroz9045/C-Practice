namespace Pms.Core.Entities
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string? ProjectName { get; set; }
        public int DepartmentId { get; set; }
        public int MaxHours { get; set; }
        
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public override string? ToString()
        {
            return $"{ProjectID}\t\t{DepartmentId}\t\t{MaxHours}\t\t{StartDate}\t\t{EndDate}\t{ProjectName}";
        }
    }
}
