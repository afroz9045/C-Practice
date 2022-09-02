using System;
using System.Collections.Generic;

namespace LibraryManagement.Core.Entities
{
    public partial class Staff
    {
        public string StaffId { get; set; } = null!;
        public string StaffName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? DesignationId { get; set; }

        public virtual Designation? Designation { get; set; }
    }
}