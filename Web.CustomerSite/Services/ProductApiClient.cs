using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.ShareModels;

namespace Web.CustomerSite.Services
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ProductApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<IList<Product>> GetProduct()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["Domain:Default"]+"/api/v1/Product");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<Product>>();
        }

        public async Task<Product> GetProductById(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["Domain:Default"] + "/api/v1/Product/" + id);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<Product>();
        }

        public async Task<IList<Product>> GetProductByCategory(string categoryName)
        {
            var client = _httpClientFactory.CreateClient();
            string temp = _configuration["Domain:Default"] + "/api/v1/Product/category=" + categoryName;
            var response = await client.GetAsync(temp);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<Product>>();
        }
    }

}
