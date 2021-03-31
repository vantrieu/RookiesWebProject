
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Backend.Models;

namespace Web.Backend.Interfaces
{
    public interface IProductFileImageRepository
    {
        Task<ProductFileImage> CreateAsync(ProductFileImage model);

        Task<ProductFileImage> DeleteAsync(int fileId,int productId);

        Task<List<ProductFileImage>> GetByProductId(int id);
    }
}
