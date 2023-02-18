using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords must match")]
        public string ConfirmPassword { get; set; }
    }
}
