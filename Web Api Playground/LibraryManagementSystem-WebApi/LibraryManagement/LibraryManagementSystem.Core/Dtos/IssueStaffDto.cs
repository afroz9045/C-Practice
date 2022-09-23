namespace LibraryManagement.Core.Dtos
{
    public class IssueStaffDto
    {
        public short? IssueId { get; set; }
        public int BookId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string? StaffId { get; set; }
    }
}