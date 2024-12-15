namespace HerkansingA2D1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        // Aanbiedingen functionaliteit.
        public decimal? PromotionalPrice { get; set; }
        public DateTime? PromotionStart { get; set; }
        public DateTime? PromotionEnd { get; set; }
    }
}
