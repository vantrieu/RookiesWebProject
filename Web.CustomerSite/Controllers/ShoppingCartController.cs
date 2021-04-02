using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.CustomerSite.Extentions;
using Web.CustomerSite.Services;

namespace Web.CustomerSite.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;
        private readonly IOrderApiClient _orderApiClient;

        public ShoppingCartController(IProductApiClient productApiClient, IConfiguration configuration, IOrderApiClient orderApiClient)
        {
            _productApiClient = productApiClient;
            _configuration = configuration;
            _orderApiClient = orderApiClient;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            List<int> lstCartItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            var results = await _productApiClient.GetProductByArray(lstCartItems);
            foreach (var product in results)
            {
                foreach (var item in product.ProductFileImages)
                {
                    item.FileImage.FileLocation = _configuration["Domain:Default"] + item.FileImage.FileLocation;
                }
            }
            return View(results);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public async Task<IActionResult> IndexPost()
        {
            List<int> lstCartItem = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            var result = await _orderApiClient.PostOrderAsync(lstCartItem);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            List<int> lstCartItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if (lstCartItems.Count > 0)
            {
                if (lstCartItems.Contains(id))
                {
                    lstCartItems.Remove(id);
                }
            }
            HttpContext.Session.Set("ssShoppingCart", lstCartItems);
            return RedirectToAction(nameof(Index));
        }
    }
}
