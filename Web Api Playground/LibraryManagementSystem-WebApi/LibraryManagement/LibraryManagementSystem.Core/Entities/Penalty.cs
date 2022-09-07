using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Core.Entities
{
    [Table("penalty")]
    public class Penalty
    {
        public int PenaltyId { get; set; }
        public short? IssueId { get; set; }
        public bool? PenaltyPaidStatus { get; set; }
        public int? PenaltyAmount { get; set; }

        public virtual Issue? Issue { get; set; }
    }
}