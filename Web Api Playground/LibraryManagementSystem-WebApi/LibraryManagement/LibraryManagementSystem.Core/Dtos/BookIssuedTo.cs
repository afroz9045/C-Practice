namespace LibraryManagement.Core.Dtos
{
    public class BookIssuedTo
    {
        public int BookId { get; set; }
        public string? StaffId { get; set; }
        public string? StaffName { get; set; }
        public int? StudentId { get; set; }
        public string? StudentName { get; set; }
    }
}