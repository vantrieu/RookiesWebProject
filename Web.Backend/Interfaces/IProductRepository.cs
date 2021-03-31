using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Backend.Models;
using Web.ShareModels;

namespace Web.Backend.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product model);

        Task<Product> DeleteAsync(int id);

        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetByIdAsync(int id);

        Task<IEnumerable<Product>> GetByNameAsync(string name);

        Task<Product> UpdateAsync(int id, Product model);
    }
}
