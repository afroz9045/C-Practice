namespace LibraryManagementAPI.ViewModels
{
    public class StaffVm
    {
        public Guid StaffId { get; set; }
        public string StaffName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public Guid? DesignationId { get; set; }
    }
}