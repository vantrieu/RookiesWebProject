using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.ShareModels.ViewModels;

namespace Web.Services.Interfaces
{
    public interface IOrderRepository
    {
        Task<bool> CreateAsync(List<int> productIds, string userId);

        Task<IEnumerable<OrderVm>> GetMyOrder(string userId);

        Task<bool> DeleteMyOrder(int orderId);

        Task<List<OrderResponseVM>> GetAllOrder();

        Task<bool> ConfirmOrder(int orderId);
    }
}
