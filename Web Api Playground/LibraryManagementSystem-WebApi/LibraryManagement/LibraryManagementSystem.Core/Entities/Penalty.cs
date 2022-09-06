using System;
using System.Collections.Generic;

namespace LibraryManagement.Core.Entities
{
    public class Penalty
    {
        public int PenaltyId { get; set; }
        public short? IssueId { get; set; }
        public bool? PenaltyPaidStatus { get; set; }

        public virtual Issue? Issue { get; set; }
    }
}