using System.ComponentModel.DataAnnotations.Schema; // Add this using directive

namespace HerkansingAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public required string Description { get; set; }

        public decimal Price { get; set; }

        // Aanbiedingen functionaliteit.
        public decimal? PromotionalPrice { get; set; }
        public DateTime? PromotionStart { get; set; }
        public DateTime? PromotionEnd { get; set; }

        public string? ImageUrl { get; set; } // Sets image of the product in question. 

        [NotMapped]
        public IFormFile? ImageFile { get; set; } // Optional for uploaded files
    }
}
