using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ShareModels;
using Web.ShareModels.ViewModels;

namespace Web.Services
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product model);

        Task<Product> DeleteAsync(int id);

        Task<ProductPaginationVm> GetAllAsync(PagingRequestVm pagingRequestVm);

        Task<ProductVm> GetByIdAsync(int id);

        Task<Product> FindByIdAsync(int id);

        Task<IEnumerable<Product>> GetByNameAsync(string name);

        Task<IEnumerable<ProductVm>> GetByCategoryAsync(string categoryName);

        Task<Product> UpdateAsync(int id, Product model);

        Task<bool> CheckBuyByUser(string userId, int productId);

        Task<long> GetPriceById(int productId);
    }
}
