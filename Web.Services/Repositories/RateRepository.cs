using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Services.Interfaces;
using Web.ShareModels;

namespace Web.Services.Repositories
{
    public class RateRepository : IRateRepository
    {
        private readonly ApplicationDbContext _context;

        public RateRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Rate> CreateAsync(int productid, string userId, int totalStar)
        {
            Rate rate = new Rate { ProductId = productid, UserId = userId, TotalRate = totalStar };
            var result = await _context.Rates.Where(r => r.UserId == userId && r.ProductId == productid).FirstOrDefaultAsync();
            if (result != null)
            {
                result.TotalRate = rate.TotalRate;
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Add(rate);
                await _context.SaveChangesAsync();
            }
            return rate;
        }

        public async Task<bool> DeleteRatingAsync(int productId, string userId)
        {
            Rate rate = await _context.Rates.Where(r => r.ProductId == productId && r.UserId == userId).FirstOrDefaultAsync();
            if (rate == null)
                return false;
            _context.Remove(rate);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<double> GetAvgStarAsync(int productId)
        {
            return Task.FromResult(_context.Rates.Where(r => r.ProductId == productId).Average(r => r.TotalRate));
        }
    }
}
