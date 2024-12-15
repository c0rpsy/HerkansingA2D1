using HerkansingA2D1.Models;
using Newtonsoft.Json;

namespace HerkansingA2D1.Services
{
    public interface ICartService
    {
        List<OrderItem> GetCart();
        void AddToCart(Product product, int quantity);
        void SaveCart(List<OrderItem> cart);
    }

    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartSessionKey = "Cart";

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<OrderItem> GetCart()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null)
            {
                throw new InvalidOperationException("Session is not available.");
            }

            var cartJson = session.GetString(CartSessionKey);
            return cartJson == null ? new List<OrderItem>() : JsonConvert.DeserializeObject<List<OrderItem>>(cartJson) ?? new List<OrderItem>();
        }

        public void AddToCart(Product product, int quantity)
        {
            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(i => i.ProductId == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new OrderItem { ProductId = product.Id, Product = product, Quantity = quantity });
            }
            SaveCart(cart);
        }

        public void SaveCart(List<OrderItem> cart)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null)
            {
                throw new InvalidOperationException("Session is not available.");
            }

            var cartJson = JsonConvert.SerializeObject(cart);
            session.SetString(CartSessionKey, cartJson);
        }
    }
}
