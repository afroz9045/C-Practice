namespace LibraryManagement.Core.Entities
{
    public class Issue
    {
        public Issue()
        {
            Penalties = new HashSet<Penalty>();
        }

        public short? IssueId { get; set; }
        public int BookId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int? StudentId { get; set; }
        public string? StaffId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Staff? Staff { get; set; }
        public virtual Student? Student { get; set; }
        public virtual ICollection<Penalty> Penalties { get; set; }
    }
}