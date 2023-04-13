using System.ComponentModel.DataAnnotations;

namespace IGse.Core.Dtos
{
    public class UserDataToPassAuthDto
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string role { get; set; } = null!;
    }
}
