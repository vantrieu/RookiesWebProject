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
        private readonly IProductRepository _productRepository;

        public OrderRepository(ApplicationDbContext context, IOrderDetailrepository orderDetailrepository, IProductRepository productRepository)
        {
            _context = context;
            _orderDetailrepository = orderDetailrepository;
            _productRepository = productRepository;
        }

        public async Task<bool> ConfirmOrder(int orderId)
        {
            var order = await _context.Orders.Where(o => o.Id == orderId).FirstOrDefaultAsync();
            if (order == null)
                return false;
            order.status = true;
            await _context.SaveChangesAsync();
            return true;
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
                long price = await _productRepository.GetPriceById(flag);
                await _orderDetailrepository.CreateAsync(order.Id, flag, price);
            }
            return true;
        }

        public async Task<bool> DeleteMyOrder(int orderId)
        {
            var flag = await _orderDetailrepository.OrderDetailExistsAsync(orderId);
            if(!flag)
            {
                var order = await _context.Orders.Where(od => od.Id == orderId).FirstAsync();
                _context.Remove(order);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<OrderResponseVM>> GetAllOrder()
        {
            var orders = await _context.Orders.Select(o => new OrderResponseVM
            {
                orderId = o.Id,
                orderDate = o.OrderDate,
                fullname = o.User.FullName,
                phoneNumber = o.User.PhoneNumber,
                status = o.status
            }).ToListAsync();

            foreach(var order in orders)
            {
                var orderDetails = await _context.OrderDetails.Where(od => od.OrderId == order.orderId)
                    .Select(od => new OrderDetailResponseVm
                    {
                        ProductId = od.ProductId,
                        ProductName = od.Product.Name,
                        Price = od.Price,
                        Total = od.Total
                    }).ToListAsync();
                order.products = orderDetails;
            }
            return orders;
        }

        public async Task<IEnumerable<OrderVm>> GetMyOrder(string userId)
        {
            var orders = await (from od in _context.Orders
                                join odd in _context.OrderDetails
                                on od.Id equals odd.OrderId
                                where od.UserId == userId
                                select new OrderVm
                                {
                                    orderId = od.Id,
                                    ProductId = odd.ProductId,
                                    Name = odd.Product.Name,
                                    Price = odd.Price,
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
