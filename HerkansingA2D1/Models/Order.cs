namespace HerkansingA2D1.Models
{
    public class Order
    {
        public required int Id { get; set; }
        public required string CustomerName { get; set; }
        public required string CustomerEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
    }
}
