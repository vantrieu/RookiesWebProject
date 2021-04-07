using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.CustomerSite.Services;

namespace Web.CustomerSite.Controllers
{
    public class RatingsController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;

        public RatingsController(IProductApiClient productApiClient, IConfiguration configuration)
        {
            _productApiClient = productApiClient;
            _configuration = configuration;
        }

        [Authorize]
        [Route("/Ratings/{name}-{productId}-{orderId}")]
        public async Task<IActionResult> Index(int productId, int orderId)
        {
            var result = await _productApiClient.GetProductForRating(productId);
            List<string> temp = new List<string>();
            foreach (string item in result.ProductFileImages)
            {
                temp.Add(_configuration["Domain:Default"] + item);
            }
            result.ProductFileImages = temp;
            return View(result);
        }

        public async Task<IActionResult> SetRating(int Id, int rank)
        {
            await _productApiClient.PostRating(Id, rank);
            return RedirectToAction("Index", "Order");
        }
    }
}
