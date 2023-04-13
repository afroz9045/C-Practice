namespace IGse.Core.Dtos
{
    public class AdminDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PropertyType { get; set; } = null!;
        public int NumberOfBedrooms { get; set; }
        public string? Evc { get; set; } = null;
        public int WalletAmount { get; set; }
        public string EmailId { get; set; }=null!;
        public bool IsActive { get; set; }
    }
}
