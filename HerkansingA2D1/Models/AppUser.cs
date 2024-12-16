using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HerkansingA2D1.Models
{
    public class AppUser : IdentityUser<int>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public string Role { get; set; } = "Customer";

        public List<Order>? Orders { get; set; }
    }
}