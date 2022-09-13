using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.ViewModels
{
    public class ReturnVm
    {
        [Required]
        public int BookId { get; set; }
    }
}