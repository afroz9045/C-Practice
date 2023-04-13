using System.ComponentModel.DataAnnotations;

namespace IGse.Core.Entities
{
    public class Bill
    {
        [Key]
        public int BillId { get; set; }
        public int CustomerId { get; set; }
        public int DayElectricityReading { get; set; }
        public int NightElectricityReading { get; set; }
        public int GasReading { get; set; }
        public DateTime BillMonthYear { get; set; }
        public bool IsPaid { get; set; }
        public int Amount { get; set; }
        public bool IsVoucherUsed { get; set; }
        public int? EvcId { get; set; }
        public int NumberOfDays { get; set; }
        public DateTime DueDate { get; set; }
    }
}
