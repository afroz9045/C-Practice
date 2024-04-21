using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.ViewModels
{
    public class DepartmentVm
    {
        [StringLength(50), Required]
        public string DepartmentName { get; set; } = null!;
    }
}