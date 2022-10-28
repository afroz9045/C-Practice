using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.ViewModels
{
    public class StaffUpdateVm
    {
        [StringLength(50), Required]
        public string StaffName { get; set; } = null!;

        //[StringLength(15), Required]
        //public string Gender { get; set; } = null!;

        //[StringLength(15), Required]
        //public string? DesignationId { get; set; }
    }
}