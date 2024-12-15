using System.ComponentModel.DataAnnotations;

namespace HerkansingA2D1.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The UserName field is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public string Role { get; set; } = "Customer";
    }
}
