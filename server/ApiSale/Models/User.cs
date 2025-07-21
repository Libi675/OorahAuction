using System.ComponentModel.DataAnnotations;

namespace ApiSale.Models
{
    public class User
    {
        
        public int UserId { get; set; }

        //public string UserName { get; set; } = string.Empty;
        [EmailAddress, Required]
        public string Email { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [Required, MaxLength(10)]
        public string Phone { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string City { get; set; }
        public string Role { get; set; } = "user";
    }
}
