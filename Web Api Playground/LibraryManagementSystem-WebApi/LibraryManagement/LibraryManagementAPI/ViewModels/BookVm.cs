using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.ViewModels
{
    public class BookVm
    {
        [StringLength(50), Required]
        public string BookName { get; set; } = null!;

        [Range(0, 9999999)]
        public int Isbn { get; set; }

        [StringLength(25), Required]
        public string AuthorName { get; set; } = null!;

        [StringLength(20)]
        public string? BookEdition { get; set; } = "Default";
    }
}