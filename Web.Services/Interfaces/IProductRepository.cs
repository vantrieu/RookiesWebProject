using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ShareModels;

namespace Web.Services
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product model);

        Task<Product> DeleteAsync(int id);

        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetByIdAsync(int id);

        Task<IEnumerable<Product>> GetByNameAsync(string name);

        Task<IEnumerable<Product>> GetByCategoryAsync(string categoryName);

        Task<Product> UpdateAsync(int id, Product model);

        Task<bool> CheckBuyByUser(string userId, int productId);
    }
}
