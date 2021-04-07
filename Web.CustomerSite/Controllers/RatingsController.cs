using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.CustomerSite.Services;

namespace Web.CustomerSite.Controllers
{
    public class RatingsController : Controller
    {
        private readonly IProductApiClient _productApiClient;

        public RatingsController(IProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
        }
        public async Task<IActionResult> SetRating(int Id, int rank, string name)
        {
            await _productApiClient.PostRating(Id, rank);
            return RedirectToAction("Details", "Product", new { name = name.Replace(" ", "-").ToString(), id = Id });
        }
    }
}
