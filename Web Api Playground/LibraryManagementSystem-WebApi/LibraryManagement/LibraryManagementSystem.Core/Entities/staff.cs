namespace LibraryManagement.Core.Entities
{
    public partial class Staff
    {
        public Staff()
        {
            Designations = new HashSet<Designation>();
        }

        public Guid StaffId { get; set; }
        public string StaffName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public Guid? DesignationId { get; set; }

        public virtual Designation? DesignationNavigation { get; set; }
        public virtual ICollection<Designation> Designations { get; set; }
    }
}