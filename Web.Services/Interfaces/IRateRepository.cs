using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.ShareModels;

namespace Web.Services.Interfaces
{
    public interface IRateRepository
    {
        Task<Rate> CreateAsync(int productid, string userId, int totalStar);

        Task<double> GetAvgStarAsync(int productId);
    }
}
