using System;
using System.Collections.Generic;

namespace LibraryManagement.Core.Entities
{
    public partial class Designation
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