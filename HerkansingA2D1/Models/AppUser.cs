﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HerkansingA2D1.Models
{
    public class AppUser : IdentityUser
    {
        [Required(ErrorMessage = "The UserName field is required.")]
        public override string UserName { get; set; }
        [Required(ErrorMessage = "The Password field is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public override string Email { get; set; }
        public string Role { get; set; }
    }
}
