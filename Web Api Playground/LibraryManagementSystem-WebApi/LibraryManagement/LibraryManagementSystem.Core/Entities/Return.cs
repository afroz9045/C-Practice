namespace LibraryManagement.Core.Entities
{
    public class Return
    {
        public int ReturnId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime IssueDate { get; set; }
        public int? BookId { get; set; }
        public DateTime ReturnDate { get; set; }

        public virtual Book? Book { get; set; }
    }
}