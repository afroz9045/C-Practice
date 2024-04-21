namespace LibraryManagement.Core.Dtos
{
    public class PendingBookReturnDto
    {
        public int IssueId { get; set; }
        public int BookId { get; set; }
        public string? StaffId { get; set; }
        public int? StudentId { get; set; }
    }
}