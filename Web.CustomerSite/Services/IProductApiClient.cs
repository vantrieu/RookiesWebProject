using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ShareModels;
using Web.ShareModels.ViewModels;

namespace Web.CustomerSite.Services
{
    public interface IProductApiClient
    {
        Task<IList<ProductVm>> GetProduct();

        Task<Product> GetProductById(int id);

        Task<IList<Product>> GetProductByCategory(string categoryName);

        Task<IList<Product>> GetProductByArray(List<int> temp);

        Task<Rate> PostRating(int id, int rank);
    }
}
