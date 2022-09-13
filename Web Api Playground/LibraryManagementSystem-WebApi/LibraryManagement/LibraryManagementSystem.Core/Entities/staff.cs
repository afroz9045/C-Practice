namespace LibraryManagement.Core.Entities
{
    public class Staff
    {
        public Staff()
        {
            Issues = new HashSet<Issue>();
        }

        public string? StaffId { get; set; } = null!;
        public string StaffName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? DesignationId { get; set; }

        public virtual Designation? Designation { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
    }
}