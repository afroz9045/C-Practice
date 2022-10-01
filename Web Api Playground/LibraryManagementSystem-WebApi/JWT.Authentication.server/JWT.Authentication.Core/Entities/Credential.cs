using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JWT.Authentication.Core.Entities
{
    public partial class Credential
    {
        [Key]
        public int UserId { get; set; }

        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string SaltedPassword { get; set; } = null!;
    }
}