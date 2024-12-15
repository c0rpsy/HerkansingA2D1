namespace HerkansingA2D1.Models
{
    public class Order
    {
        public required int Id { get; set; }

        public required string CustomerName { get; set; }

        public required string CustomerEmail { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }



}
