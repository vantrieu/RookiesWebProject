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

        Task<ProductVm> GetProductById(int id);

        Task<IList<ProductVm>> GetProductByCategory(string categoryName);

        Task<IList<ProductVm>> GetProductByArray(List<int> temp);

        Task<Rate> PostRating(int id, int rank);

        Task<ProductVm> GetProductForRating(int id);
    }
}
