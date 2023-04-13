using System.ComponentModel.DataAnnotations;

namespace IGse.Core.Entities
{
    public class Evc
    {
        [Key]
        public int EvcId { get; set; }
        public string EvcVoucher { get; set; } = null!;
        public bool IsUsed { get; set; }
        public int? UsedByCustomer { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
