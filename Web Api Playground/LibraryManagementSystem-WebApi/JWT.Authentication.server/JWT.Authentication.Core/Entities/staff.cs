using System;
using System.Collections.Generic;

namespace JWT.Authentication.Core.Entities
{
    public partial class staff
    {
        public string StaffId { get; set; } = null!;
        public string StaffName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? DesignationId { get; set; }
    }
}
