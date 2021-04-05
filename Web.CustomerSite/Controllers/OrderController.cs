using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.CustomerSite.Services;

namespace Web.CustomerSite.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderApiClient _orderApiClient;
        private readonly IConfiguration _configuration;

        public OrderController(IOrderApiClient orderApiClient, IConfiguration configuration)
        {
            _orderApiClient = orderApiClient;
            _configuration = configuration;
        }

        [Authorize]
        public async Task<IActionResult> IndexAsync()
        {
            var results = await _orderApiClient.GetMyOrder();
            foreach(var item in results)
            {
                item.ImgUrl = _configuration["Domain:Default"] + item.ImgUrl;
            }
            return View(results);
        }
    }
}
