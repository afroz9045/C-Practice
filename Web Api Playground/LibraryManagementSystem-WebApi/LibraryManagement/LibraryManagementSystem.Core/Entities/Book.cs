namespace LibraryManagement.Core.Entities
{
    public partial class Book
    {
        //public Book()
        //{
        //    //Issues = new HashSet<Issue>();
        //    // Returns = new HashSet<Return>();
        //}

        public int BookId { get; set; }
        public string BookName { get; set; } = null!;
        public int Isbn { get; set; }
        public string AuthorName { get; set; } = null!;
        public string? BookEdition { get; set; }
        public int? StockAvailable { get; set; }

        public ICollection<Issue>? Issues { get; set; }
        public ICollection<Return>? Returns { get; set; }
    }
}