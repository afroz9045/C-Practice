namespace JWT.Authentication.Core.Entities
{
    public class Credential
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string SaltedPassword { get; set; } = null!;
        public string StaffId { get; set; } = null!;
    }
}