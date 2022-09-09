namespace LibraryManagement.Api.ViewModels
{
    public class StaffVm
    {
        public string StaffId { get; set; }
        public string StaffName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? DesignationId { get; set; }
    }
}