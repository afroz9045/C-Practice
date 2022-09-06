using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Core.Entities
{
    public class Department
    {
        public Department()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        public short DeptId { get; set; }

        public string DepartmentName { get; set; } = null!;

        public virtual ICollection<Student> Students { get; set; }
    }
}