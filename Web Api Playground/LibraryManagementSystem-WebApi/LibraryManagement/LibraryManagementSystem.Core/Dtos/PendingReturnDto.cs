namespace LibraryManagement.Core.Dtos
{
    public class PendingReturnDto
    {
        public int IssueId { get; set; }
        public int BookId { get; set; }
        public string? Id { get; set; }
        public string? IssuedTo { get; set; }
    }
}