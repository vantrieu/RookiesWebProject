using System.Collections.Generic;
using System.Threading.Tasks;
using Web.ShareModels;

namespace Web.Backend.Interfaces
{
    public interface IProductFileImageRepository
    {
        Task<ProductFileImage> CreateAsync(ProductFileImage model);

        Task<ProductFileImage> DeleteAsync(int fileId,int productId);

        Task<List<ProductFileImage>> GetByProductId(int id);
    }
}
