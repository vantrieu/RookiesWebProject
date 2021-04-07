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
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;

        public ProductController(IProductApiClient productApiClient, IConfiguration configuration)
        {
            _productApiClient = productApiClient;
            _configuration = configuration;
        }

        [Route("/Product/{name}_{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _productApiClient.GetProductById(id);
            List<string> temp = new List<string>();
            foreach (string item in result.ProductFileImages)
            {
                temp.Add(_configuration["Domain:Default"] + item);
            }
            result.ProductFileImages = temp;
            return View(result);
        }

        [Route("/Product/{categoryName}")]
        public async Task<IActionResult> Finds(string categoryName)
        {
            var results = await _productApiClient.GetProductByCategory(categoryName);
            foreach (var product in results)
            {
                List<string> temp = new List<string>();
                foreach (string item in product.ProductFileImages)
                {
                    temp.Add(_configuration["Domain:Default"] + item);
                }
                product.ProductFileImages = temp;
            }
            return View(results);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        [Route("/Product/{name}_{id}")]
        public IActionResult DetailsPost(int id, string name)
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if (lstShoppingCart == null)
            {
                lstShoppingCart = new List<int>();
            }
            int flag = 0;
            foreach (int item in lstShoppingCart)
            {
                if (item == id)
                    flag++;
            }
            if (flag == 0)
                lstShoppingCart.Add(id);
            HttpContext.Session.Set("ssShoppingCart", lstShoppingCart);
            return RedirectToAction("Details", "Product", new { name = name, id = id });
        }

        public IActionResult Remove(int id, string name)
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if (lstShoppingCart.Count > 0)
            {
                if (lstShoppingCart.Contains(id))
                {
                    lstShoppingCart.Remove(id);
                }
            }
            HttpContext.Session.Set("ssShoppingCart", lstShoppingCart);
            return RedirectToAction("Details", "Product", new { name = name, id = id }) ;
        }
    }
}
