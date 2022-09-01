namespace LibraryManagementAPI.ViewModels
{
    public class DesignationVm
    {
        public Guid DesignationId { get; set; }
        public string DesignationName { get; set; } = null!;
        public Guid StaffId { get; set; }
    }
}