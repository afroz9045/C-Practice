namespace JWT.Authentication.Server.Infrastructure.VM
{
    public class UserVm
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string role { get; set; }
    }   
}
