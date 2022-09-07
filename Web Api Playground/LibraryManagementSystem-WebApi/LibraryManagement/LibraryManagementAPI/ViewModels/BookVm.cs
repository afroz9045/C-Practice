namespace LibraryManagementAPI.ViewModels
{
    public class BookVm
    {
        public int? BookId { get; set; }
        public string BookName { get; set; } = null!;

        public int Isbn { get; set; }
        public string AuthorName { get; set; } = null!;
        public string? BookEdition { get; set; } = "Default";
    }
}