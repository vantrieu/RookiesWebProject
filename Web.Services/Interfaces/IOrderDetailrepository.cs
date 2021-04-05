using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.ShareModels;

namespace Web.Services.Interfaces
{
    public interface IOrderDetailrepository
    {
        Task<OrderDetail> CreateAsync(int orderId, int productId);

        Task<OrderDetail> DeleteAsync(int orderId, int productId);

        Task<bool> OrderDetailExistsAsync(int orderId);
    }
}
