using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ShareModels;

namespace Web.CustomerSite.Services
{
    public interface IProductApiClient
    {
        Task<IList<Product>> GetProduct();
    }
}
