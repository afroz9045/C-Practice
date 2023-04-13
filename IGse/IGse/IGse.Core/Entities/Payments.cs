using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGse.Core.Entities
{
    //[Keyless]
    public class Payments
    {
        public int CustomerId { get; set; }
        public int BillId { get; set; }
        public bool PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; } 
    }
}
