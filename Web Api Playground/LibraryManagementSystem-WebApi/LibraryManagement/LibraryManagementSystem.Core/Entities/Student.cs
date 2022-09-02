using System;
using System.Collections.Generic;

namespace LibraryManagement.Core.Entities
{
    public partial class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string StudentDepartment { get; set; } = null!;
        public short? DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
    }
}
