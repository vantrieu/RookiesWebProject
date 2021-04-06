using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Services.Interfaces;
using Web.ShareModels;

namespace Web.Services.Repositories
{
    public class OrderDetailrepository : IOrderDetailrepository
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailrepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<OrderDetail> CreateAsync(int orderId, int productId, long price)
        {
            OrderDetail orderDetail = new OrderDetail { OrderId = orderId, ProductId = productId, Total = 1, Price = price };
            _context.Add(orderDetail);
            await _context.SaveChangesAsync();
            return orderDetail;
        }

        public async Task<OrderDetail> DeleteAsync(int orderId, int productId)
        {
            OrderDetail orderDetail = _context.OrderDetails.Where(odd => odd.OrderId == orderId && odd.ProductId == productId).FirstOrDefault();
            if(orderDetail != null)
            {
                _context.Remove(orderDetail);
                await _context.SaveChangesAsync();
                return orderDetail;
            }
            return null;
        }

        public async Task<bool> OrderDetailExistsAsync(int orderId)
        {
            var results = await _context.OrderDetails.Where(odd => odd.OrderId == orderId).ToListAsync();
            if (results.Count != 0)
                return true;
            return false;
        }
    }
}
