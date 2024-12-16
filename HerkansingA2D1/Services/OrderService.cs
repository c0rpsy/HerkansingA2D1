using HerkansingA2D1.Data;
using HerkansingA2D1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerkansingA2D1.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersByUserIdAsync(int userId);
    }

    public class OrderService : IOrderService
    {
        private readonly HerkansingA2D1Context _context;

        public OrderService(HerkansingA2D1Context context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }
    }
}
