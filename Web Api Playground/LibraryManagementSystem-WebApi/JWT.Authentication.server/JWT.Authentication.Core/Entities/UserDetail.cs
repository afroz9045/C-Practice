using System;
using System.Collections.Generic;

namespace JWT.Authentication.Core.Entities
{
    public partial class UserDetail
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string SaltedPassword { get; set; } = null!;
        public string StaffId { get; set; } = null!;
    }
}