using System.ComponentModel.DataAnnotations;

namespace IGse.Core.Entities
{
    public class CustomerEvcHistory
    {
        [Key]
        public int HistoryId { get; set; }
        public int CustomerId { get; set; }
        public int EvcId { get; set; }
        public DateTime DateOfUsed { get; set; }

    }
}
