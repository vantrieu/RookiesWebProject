using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Services.Interfaces;
using Web.ShareModels;

namespace Web.Services.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IOrderDetailrepository _orderDetailrepository;

        public OrderRepository(ApplicationDbContext context, IOrderDetailrepository orderDetailrepository)
        {
            _context = context;
            _orderDetailrepository = orderDetailrepository;
        }

        public async Task<bool> CreateAsync(List<int> productIds, string userId)
        {
            foreach (int flag in productIds)
            {
                Order order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Today,
                    status = false
                };
                _context.Add(order);
                await _context.SaveChangesAsync();
                _orderDetailrepository.CreateAsync(order.Id, flag);
            }
            return true;
        }
    }
}
