using System.Collections.Generic;
using System.Threading.Tasks;
using Web.ShareModels;

namespace Web.Services
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
