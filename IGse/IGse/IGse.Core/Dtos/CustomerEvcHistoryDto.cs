using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGse.Core.Dtos
{
    public class CustomerEvcHistoryDto
    {
        public int HistoryId { get; set; }
        public int CustomerId { get; set; }
        public int EvcId { get; set; }
        public int Amount { get; set; }
        public DateTime DateOfUsed { get; set; }
    }
}
