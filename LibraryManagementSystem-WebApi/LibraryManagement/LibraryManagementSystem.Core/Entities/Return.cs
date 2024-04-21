namespace LibraryManagement.Core.Entities
{
    public partial class Return
    {
        public int ReturnId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime IssueDate { get; set; }
        public int? BookId { get; set; }
        public DateTime ReturnDate { get; set; }
        public short? IssueId { get; set; }

        public virtual Issue? Issue { get; set; }
        public virtual Book? Book { get; set; }
    }
}