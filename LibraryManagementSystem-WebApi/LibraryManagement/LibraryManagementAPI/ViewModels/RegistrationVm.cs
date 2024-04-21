namespace LibraryManagement.Api.ViewModels
{
    public class RegistrationVm
    {
        public string FullName { get; set; } = null!;
        public string StaffId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}