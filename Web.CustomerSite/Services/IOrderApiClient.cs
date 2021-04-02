using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.CustomerSite.Services
{
    public interface IOrderApiClient
    {
        Task<bool> PostOrderAsync(List<int> productIds);
    }
}
