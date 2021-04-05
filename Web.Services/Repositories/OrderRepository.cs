using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Services.Interfaces;
using Web.ShareModels;
using Web.ShareModels.ViewModels;

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
            Order order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Today,
                status = false
            };
            _context.Add(order);
            await _context.SaveChangesAsync();
            foreach (int flag in productIds)
            {
                await _orderDetailrepository.CreateAsync(order.Id, flag);
            }
            return true;
        }

        public async Task<IEnumerable<OrderVm>> GetMyOrder(string userId)
        {
            var orders = await (from od in _context.Orders
                                join odd in _context.OrderDetails
                                on od.Id equals odd.OrderId
                                select new OrderVm
                                {
                                    orderId = od.Id,
                                    ProductId = odd.ProductId,
                                    Name = odd.Product.Name,
                                    Price = odd.Product.Price,
                                    OrderDate = DateTimeOffset.Parse(od.OrderDate.ToString()),
                                    Status = od.status
                                }).OrderBy(odv => odv.orderId).ToListAsync();
            foreach (OrderVm order in orders)
            {
                var result = await _context.ProductFileImages.Include(pfi => pfi.FileImage).Where(pfi => pfi.ProductId == order.ProductId).FirstAsync();
                order.ImgUrl = result.FileImage.FileLocation;
            }
            return orders;
        }
    }
}
