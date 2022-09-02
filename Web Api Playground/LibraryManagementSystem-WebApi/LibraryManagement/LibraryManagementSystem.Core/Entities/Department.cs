using System;
using System.Collections.Generic;

namespace LibraryManagement.Core.Entities
{
    public partial class Department
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
