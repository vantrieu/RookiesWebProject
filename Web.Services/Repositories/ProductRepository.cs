using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ShareModels;
using Web.ShareModels.ViewModels;

namespace Web.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckBuyByUser(string userId, int productId)
        {
            var orders = await _context.Orders.Where(od => od.UserId == userId).ToArrayAsync();
            if (orders.Count() != 0)
            {
                foreach (var order in orders)
                {
                    var total = await _context.OrderDetails.Where(odd => odd.ProductId == productId && odd.OrderId == order.Id).CountAsync();
                    if (total > 0)
                        return true;
                }
            }
            return false;
        }

        public async Task<Product> CreateAsync(Product model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Product> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return null;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            var product = await _context.Products.Include(p => p.ProductFileImages).ThenInclude(pfi => pfi.FileImage)
                .Include(p => p.Category).Where(p => p.Id == id).FirstOrDefaultAsync();
            return product;
        }

        public async Task<IEnumerable<ProductVm>> GetAllAsync()
        {
            var products = await _context.Products.Include(p => p.Category).Select(p =>
                new ProductVm
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Quantities = p.Quantities,
                    Price = p.Price,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    CategoryName = p.Category.Name
                }).ToListAsync();
            foreach (var product in products)
            {
                var images = await _context.ProductFileImages.Where(p => p.ProductId == product.Id).Select(pfi => pfi.FileImage.FileLocation).ToListAsync();
                product.ProductFileImages = images;
                var rates = await _context.Rates.Where(r => r.ProductId == product.Id).Select(r => r.TotalRate).ToListAsync();
                product.Rates = rates;
            }
            return products;
        }

        public async Task<IEnumerable<ProductVm>> GetByCategoryAsync(string categoryName)
        {
            var products = await _context.Products.Include(p => p.Category).Where(p => p.Category.Name == categoryName).Select(p =>
                new ProductVm
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Quantities = p.Quantities,
                    Price = p.Price,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    CategoryName = p.Category.Name
                }).ToListAsync();
            foreach (var product in products)
            {
                var images = await _context.ProductFileImages.Where(p => p.ProductId == product.Id).Select(pfi => pfi.FileImage.FileLocation).ToListAsync();
                product.ProductFileImages = images;
                var rates = await _context.Rates.Where(r => r.ProductId == product.Id).Select(r => r.TotalRate).ToListAsync();
                product.Rates = rates;
            }
            return products;
        }

        public async Task<ProductVm> GetByIdAsync(int id)
        {
            var product = await _context.Products.Include(p => p.Category).Where(p => p.Id == id).Select(p =>
                new ProductVm
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Quantities = p.Quantities,
                    Price = p.Price,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    CategoryName = p.Category.Name
                }).FirstOrDefaultAsync();
            var images = await _context.ProductFileImages.Where(p => p.ProductId == product.Id).Select(pfi => pfi.FileImage.FileLocation).ToListAsync();
            product.ProductFileImages = images;
            var rates = await _context.Rates.Where(r => r.ProductId == product.Id).Select(r => r.TotalRate).ToListAsync();
            product.Rates = rates;
            return product;
        }

        public async Task<IEnumerable<Product>> GetByNameAsync(string name)
        {
            var products = await _context.Products.Include(p => p.ProductFileImages).ThenInclude(pfi => pfi.FileImage)
               .Include(p => p.Category).Where(c => c.Name.ToLower().Contains(name.ToLower())).ToListAsync();
            return products;
        }

        public async Task<long> GetPriceById(int productId)
        {
            var price = await _context.Products.Where(p => p.Id == productId).Select(p => p.Price).FirstAsync();
            return price;
        }

        public async Task<Product> UpdateAsync(int id, Product model)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }
            product.Name = model.Name;
            product.Description = model.Description;
            product.Quantities = model.Quantities;
            product.Price = model.Price;
            //product.FileImageId = model.FileImageId;
            product.UpdatedDate = DateTime.Today;
            product.CategoryId = model.CategoryId;
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
