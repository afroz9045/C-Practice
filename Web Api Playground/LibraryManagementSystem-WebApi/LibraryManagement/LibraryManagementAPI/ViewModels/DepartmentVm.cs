using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.ViewModels
{
    public class DepartmentVm
    {
        [StringLength(20), Required]
        public string DepartmentName { get; set; } = null!;
    }
}