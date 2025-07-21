using System.ComponentModel.DataAnnotations;

namespace ApiSale.Models.ModelDTO
{
    public class UserDTO
    {
        public string UserName { get; set; }
        [MaxLength(10)]
        public string Password { get; set; }
        [MaxLength(40)]
        public string FullName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(10)]
        public string Phone { get; set; }
        public bool IsAdmin { get; set; }
    }
}
