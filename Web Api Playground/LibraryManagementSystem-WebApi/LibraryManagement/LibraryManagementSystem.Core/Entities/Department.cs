﻿namespace LibraryManagement.Core.Entities
{
    public class Department
    {
        public Department()
        {
            Students = new HashSet<Student>();
        }

        public short DeptId { get; set; }
        public string DepartmentName { get; set; } = null!;
        public int? StudentId { get; set; }

        public virtual Student? Student { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
