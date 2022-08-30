namespace LibraryManagement.Core.Dtos
{
    public class StudentDto
    {
        public int? StudentId { get; set; }
        public string StudentName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? StudentDepartment { get; set; }
        public short DepartmentId { get; set; }
    }
}