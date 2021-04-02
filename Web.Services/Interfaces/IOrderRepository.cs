using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Web.Services.Interfaces
{
    public interface IOrderRepository
    {
        Task<bool> CreateAsync(List<int> productIds, string userId);
    }
}
