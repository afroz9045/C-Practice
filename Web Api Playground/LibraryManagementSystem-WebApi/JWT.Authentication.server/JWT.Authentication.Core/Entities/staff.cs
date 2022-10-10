using System;
using System.Collections.Generic;

namespace JWT.Authentication.Core.Entities
{
    public class Staff
    {
        public string StaffId { get; set; } = null!;
        public string StaffName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? DesignationId { get; set; }
    }
}