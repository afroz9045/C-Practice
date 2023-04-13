using System.ComponentModel.DataAnnotations;

namespace IGse.Core.Entities
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string SaltedPassword { get; set; }
        public string role { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }


    }
}
