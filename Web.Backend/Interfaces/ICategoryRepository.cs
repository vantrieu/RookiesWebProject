using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Backend.Models;

namespace Web.Backend.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category> GetByIdAsync(int id);

        Task<IEnumerable<Category>> GetByNameAsync(string name);

        Task<int> CreateAsync(Category productType);

        Task<int> UpdateAsync(int id, Category productType);

        Task<int> DeleteAsync(int id);
    }
}
