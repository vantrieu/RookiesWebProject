using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web.CustomerSite.Models;
using Web.CustomerSite.Services;
using Web.ShareModels.ViewModels;

namespace Web.CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IProductApiClient productApiClient, IConfiguration configuration)
        {
            _logger = logger;
            _productApiClient = productApiClient;
            _configuration = configuration;
        }

        [Route("")]
        [Route("Home-pageNumber-{pageNumber}")]
        public async Task<IActionResult> Index(int? pageNumber)
        {
            ProductPaginationVm result = await _productApiClient.GetProduct(pageNumber);
            foreach (var product in result.items)
            {
                List<string> temp = new List<string>();
                foreach (string item in product.ProductFileImages)
                {
                    temp.Add(_configuration["Domain:Default"] + item);
                }
                product.ProductFileImages = temp;
            }
            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
