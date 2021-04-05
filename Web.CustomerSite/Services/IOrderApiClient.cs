using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ShareModels.ViewModels;

namespace Web.CustomerSite.Services
{
    public interface IOrderApiClient
    {
        Task<bool> PostOrderAsync(List<int> productIds);

        Task<IList<OrderVm>> GetMyOrder();
    }
}
