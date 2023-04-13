using System.ComponentModel.DataAnnotations;

namespace IGse.Core.Entities
{
    public class Customers
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PropertyType { get; set; } = null!;
        public int NumberOfBedrooms { get; set; }
        public string? Evc { get; set; }=null;
        public int  WalletAmount { get; set; }
    }
}
