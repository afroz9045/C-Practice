using System.ComponentModel.DataAnnotations;

namespace Catalog.Vms
{
    public record AddItemVm
    {
        [Required]
        public string Name { get; init; }
        [Required,Range(1,1000)]
        public decimal Price { get; init; }
    }
}