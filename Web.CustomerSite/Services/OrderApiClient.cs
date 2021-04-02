using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Web.CustomerSite.Services
{
    public class OrderApiClient : IOrderApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public OrderApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<bool> PostOrderAsync(List<int> productIds)
        {
            var client = _httpClientFactory.CreateClient();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(productIds),
                Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_configuration["Domain:Default"] + "/api/v1/Order", httpContent);
            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
