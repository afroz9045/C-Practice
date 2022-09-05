namespace LibraryManagement.Core.Entities
{
    public partial class Return
    {
        public int? ReturnId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime IssueDate { get; set; }
        public int? BookId { get; set; }

        public Book? Book { get; set; }
    }
}