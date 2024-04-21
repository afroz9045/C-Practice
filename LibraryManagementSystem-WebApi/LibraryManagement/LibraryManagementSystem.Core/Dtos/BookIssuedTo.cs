namespace LibraryManagement.Core.Dtos
{
    public class BookIssuedTo
    {
        public short? IssueId { get; set; }
        public int BookId { get; set; }
        public string? BookName { get; set; }
        public string? StaffId { get; set; }
        public string? StaffName { get; set; }
        public int? StudentId { get; set; }
        public string? StudentName { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}