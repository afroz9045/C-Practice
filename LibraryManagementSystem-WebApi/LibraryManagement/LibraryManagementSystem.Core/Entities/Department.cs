namespace LibraryManagement.Core.Entities
{
    public class Department
    {
        public Department()
        {
            Students = new HashSet<Student>();
        }

        public short DeptId { get; set; }
        public string DepartmentName { get; set; } = null!;

        public virtual ICollection<Student> Students { get; set; }
    }
}