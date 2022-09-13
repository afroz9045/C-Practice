using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.ViewModels
{
    public class IssueVm
    {
        [Required]
        public int BookId { get; set; }

        [Range(0, 10000)]
        public int? StudentId { get; set; }

        [StringLength(15)]
        public string? StaffId { get; set; }
    }
}