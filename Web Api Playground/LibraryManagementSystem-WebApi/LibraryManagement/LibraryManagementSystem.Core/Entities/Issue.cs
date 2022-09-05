namespace LibraryManagement.Core.Entities
{
    public partial class Issue
    {
        public short IssueId { get; set; }
        public int BookId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Book? Book { get; set; }
    }
}