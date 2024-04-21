namespace LibraryManagement.Core.Dtos
{
    public class ReturnDto
    {
        public int ReturnId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime IssueDate { get; set; }
        public int? BookId { get; set; }
        public DateTime ReturnDate { get; set; }
        public short? IssueId { get; set; }
    }
}