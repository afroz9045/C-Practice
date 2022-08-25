using System;
using System.Collections.Generic;

namespace LibraryManagement.Core.Entities
{
    public partial class staff
    {
        public staff()
        {
            Designations = new HashSet<Designation>();
        }

        public string StaffId { get; set; } = null!;
        public string StaffName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? Designation { get; set; }

        public virtual Designation? DesignationNavigation { get; set; }
        public virtual ICollection<Designation> Designations { get; set; }
    }
}
