namespace LibraryManagement.Core.Entities
{
    public class Student
    {
        public Student()
        {
            Issues = new HashSet<Issue>();
        }

        public int StudentId { get; set; }
        public string StudentName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string StudentDepartment { get; set; } = null!;
        public short? DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
    }
}