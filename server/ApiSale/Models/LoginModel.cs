using System.ComponentModel.DataAnnotations;

namespace ApiSale.Models
{
    public class LoginModel
    {
        public int Id { get; set; }

        [EmailAddress,Required]
        public string Email { get; set; }

        [Required,MinLength(6)]
        public  string Password { get; set; }
    }
}
