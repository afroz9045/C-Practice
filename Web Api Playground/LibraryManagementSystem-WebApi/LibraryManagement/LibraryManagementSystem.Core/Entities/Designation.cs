namespace LibraryManagement.Core.Entities
{
    public partial class Designation
    {
        public Designation()
        {
            staff = new HashSet<Staff>();
        }

        public Guid DesignationId { get; set; }
        public string DesignationName { get; set; } = null!;
        public Guid StaffId { get; set; }

        public virtual Staff? Staff { get; set; }
        public virtual ICollection<Staff> staff { get; set; }
    }
}