namespace JWT.Authentication.server.Infrastructure.Vm
{
    public class RegistrationVm
    {
        public string FullName { get; set; } = null!;
        public string StaffId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}