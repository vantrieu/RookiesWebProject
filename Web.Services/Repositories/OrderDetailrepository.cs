using System;
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
        public async Task<OrderDetail> CreateAsync(int orderId, int productId)
        {
            OrderDetail orderDetail = new OrderDetail { OrderId = orderId, ProductId = productId, Total = 1 };
            _context.Add(orderDetail);
            await _context.SaveChangesAsync();
            return orderDetail;
        }
    }
}
