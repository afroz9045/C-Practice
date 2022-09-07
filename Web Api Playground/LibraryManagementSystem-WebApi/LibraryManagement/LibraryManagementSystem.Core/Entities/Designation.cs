using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Core.Entities
{
    [Table("designation")]
    public class Designation
    {
        public Designation()
        {
            staff = new HashSet<Staff>();
        }

        public string DesignationId { get; set; } = null!;
        public string DesignationName { get; set; } = null!;

        public virtual ICollection<Staff> staff { get; set; }
    }
}