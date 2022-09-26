namespace LibraryManagement.Core.Dtos
{
    public class IssueStudentOrStaffDto
    {
        public short? IssueId { get; set; }
        public int BookId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Id { get; set; }

        public string? IssuedTo { get; set; }
    }
}