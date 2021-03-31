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

        Task<Category> CreateAsync(Category model);

        Task<Category> UpdateAsync(int id, Category model);

        Task<Category> DeleteAsync(int id);
    }
}
