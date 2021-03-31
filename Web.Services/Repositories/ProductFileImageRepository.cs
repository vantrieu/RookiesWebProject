using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ShareModels;

namespace Web.Services
{
    public class ProductFileImageRepository : IProductFileImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductFileImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ProductFileImage> CreateAsync(ProductFileImage model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<ProductFileImage> DeleteAsync(int fileId, int productId)
        {
            var result = await _context.ProductFileImages.Where(pfi => pfi.ProductId == productId && pfi.FileImageId == fileId)
                .FirstOrDefaultAsync();
            if (result == null)
                return null;
            _context.ProductFileImages.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<List<ProductFileImage>> GetByProductId(int id)
        {
            var productFileImageIds = await _context.ProductFileImages.Where(pfi => pfi.ProductId == id).ToListAsync();
            return productFileImageIds;
        }
    }
}
