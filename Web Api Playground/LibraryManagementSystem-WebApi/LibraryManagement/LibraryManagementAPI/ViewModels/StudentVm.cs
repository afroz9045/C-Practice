namespace LibraryManagementAPI.ViewModels
{
    public class StudentVm
    {
        public string StudentName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public short DepartmentId { get; set; }
    }
}