using System.ComponentModel.DataAnnotations;

namespace IGse.Core.Entities
{
    public class SetPrice
    {
        [Key]
        public int SetId { get; set; }
        public DateTime SetDate { get; set; }
        public decimal ElectricityPriceNight { get; set; }
        public decimal ElectricityPriceDay { get; set; }
        public decimal GasPrice { get; set; }
        public decimal StandingCharge { get; set; }
    }
}
