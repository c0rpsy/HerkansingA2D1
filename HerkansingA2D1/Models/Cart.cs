using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HerkansingA2D1.Models
{
    public class Cart
    {
        public int Id { get; set; }
        [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
