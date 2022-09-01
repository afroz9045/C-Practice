namespace LibraryManagementAPI.ViewModels
{
    public class BookVm
    {
        public string BookName { get; set; } = null!;
        public int Isbn { get; set; }
        public string AuthorName { get; set; } = null!;
        public string BookEdition { get; set; } = null!;
    }
}