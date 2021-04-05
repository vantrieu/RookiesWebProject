using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.CustomerSite.Extentions;
using Web.ShareModels;

namespace Web.CustomerSite.Services
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ITokenServices _tokenServices;

        public ProductApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, ITokenServices tokenServices)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _tokenServices = tokenServices;
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

        public async Task<IList<Product>> GetProductByArray(List<int> temp)
        {
            List<Product> lstProduct = new List<Product>();
            var client = _httpClientFactory.CreateClient();
            if (temp == null)
                return lstProduct;
            foreach (int id in temp)
            {
                string result = _configuration["Domain:Default"] + "/api/v1/Product/" + id;
                var response = await client.GetAsync(result);
                response.EnsureSuccessStatusCode();
                lstProduct.Add(await response.Content.ReadAsAsync<Product>());
            }
            return lstProduct;
        }

        public async Task<Rate> PostRating(int id, int rank)
        {
            var client = _httpClientFactory.CreateClient();
            var accessToken = await _tokenServices.RefreshTokenAsync();
            client.UseBearerToken(accessToken);
            var response = await client.PostAsync(_configuration["Domain:Default"] + "/api/v1/Product/rate?productId="
                + id + "&totalStar=" + rank, null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<Rate>();

        }
    }

}
