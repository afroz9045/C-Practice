namespace LibraryManagement.Core.Dtos
{
    public class PenaltyDto
    {
        public int? PenaltyId { get; set; }
        public short? IssueId { get; set; }
        public bool? PenaltyPaidStatus { get; set; }
        public int? PenaltyAmount { get; set; }
    }
}