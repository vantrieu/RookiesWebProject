using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.CustomerSite.Services;

namespace Web.CustomerSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;

        public ProductController(IProductApiClient productApiClient, IConfiguration configuration)
        {
            _productApiClient = productApiClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _productApiClient.GetProductById(id);
            foreach(var item in result.ProductFileImages)
            {
                item.FileImage.FileLocation = _configuration["Domain:Default"] + item.FileImage.FileLocation;
            }
            return View(result);
        }
    }
}
