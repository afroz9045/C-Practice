using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.ViewModels
{
    public class StaffVm
    {
        [StringLength(20), Required]
        public string StaffName { get; set; } = null!;

        [StringLength(15), Required]
        public string Gender { get; set; } = null!;

        [StringLength(15), Required]
        public string? DesignationId { get; set; }

        [StringLength(50), Required]
        public string Email { get; set; } = null!;

        [StringLength(50), Required]
        public string Password { get; set; } = null!;
    }
}