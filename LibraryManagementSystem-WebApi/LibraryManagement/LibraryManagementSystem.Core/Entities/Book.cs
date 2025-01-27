﻿namespace LibraryManagement.Core.Entities
{
    public class Book
    {
        public Book()
        {
            Issues = new HashSet<Issue>();
            Returns = new HashSet<Return>();
        }

        public int? BookId { get; set; }
        public string BookName { get; set; } = null!;
        public int Isbn { get; set; }
        public string AuthorName { get; set; } = null!;
        public string BookEdition { get; set; } = "Default";
        public int? StockAvailable { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }
        public virtual ICollection<Return> Returns { get; set; }
    }
}