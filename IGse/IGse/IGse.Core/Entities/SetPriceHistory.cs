namespace IGse.Core.Entities
{
    public class SetPriceHistory
    {
        public int Id { get; set; }
        public string SetType { get; set; } = null!;
        public DateTime SetDate { get; set; }
        public int SetBy { get; set; }
    }
}
