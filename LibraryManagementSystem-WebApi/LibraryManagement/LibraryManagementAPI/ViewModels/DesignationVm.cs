using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.ViewModels
{
    public class DesignationVm
    {
        [StringLength(20), Required]
        public string DesignationName { get; set; } = null!;
    }
}