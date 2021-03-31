using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Backend.Data;
using Web.Backend.Interfaces;
using Web.Backend.Models;
using Web.ShareModels;

namespace Web.Backend.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
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

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            //var products = await _context.Products.Include(p => p.ProductFileImage).ToListAsync();
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetByNameAsync(string name)
        {
            var product = await _context.Products.Where(c => c.Name.Contains(name)).ToListAsync();
            return product;
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
