namespace LibraryManagement.Core.Dtos
{
    public class StaffDto
    {
        public string? StaffId { get; set; } = null!;
        public string StaffName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? DesignationId { get; set; }
    }
}