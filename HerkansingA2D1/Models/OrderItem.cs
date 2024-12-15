namespace HerkansingA2D1.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
        public Product Product { get; set; }
        public decimal? PromotionPrice { get; set; }
    }
}
