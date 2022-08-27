namespace LibraryManagement.Core.Entities
{
    public class Book
    {
        public Book()
        {
            //Issues = new HashSet<Issue>();
            //Returns = new HashSet<Return>();
        }

        public int BookId { get; set; }
        public string BookName { get; set; } = null!;
        public int Isbn { get; set; }
        public string AuthorName { get; set; } = null!;
        public string BookEdition { get; set; } = null!;

        public virtual List<Issue> Issues { get; set; }
        public virtual List<Return> Returns { get; set; }
    }
}
