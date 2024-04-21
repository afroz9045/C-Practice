namespace LibraryManagement.Core.Dtos
{
    public class PenaltyCalculationDto
    {
        //public short? IssueId { get; set; }
        //public int PenaltyId { get; set; }
        public bool? PenaltyPaidStatus { get; set; }

        public int? PenaltyAmount { get; set; }

        //public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}