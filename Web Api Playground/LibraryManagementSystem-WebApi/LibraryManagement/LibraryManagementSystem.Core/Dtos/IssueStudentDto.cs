namespace LibraryManagement.Core.Dtos
{
    public class IssueStudentDto
    {
        public short? IssueId { get; set; }
        public int BookId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int? StudentId { get; set; }
    }
}