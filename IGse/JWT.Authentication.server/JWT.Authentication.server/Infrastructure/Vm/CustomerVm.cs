using System.ComponentModel.DataAnnotations;

namespace IGse.ViewModels
{
    public class CustomerVm
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string PropertyType { get; set; } = null!;
        [Required]
        public int NumberOfBedrooms { get; set; }
        public string? Evc { get; set; }
    }
}
