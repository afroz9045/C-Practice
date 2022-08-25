using System;
using System.Collections.Generic;

namespace LibraryManagement.Core.Entities
{
    public partial class Designation
    {
        public Designation()
        {
            staff = new HashSet<staff>();
        }

        public string DesignationId { get; set; } = null!;
        public string Designation1 { get; set; } = null!;
        public string? StaffId { get; set; }

        public virtual staff? Staff { get; set; }
        public virtual ICollection<staff> staff { get; set; }
    }
}
