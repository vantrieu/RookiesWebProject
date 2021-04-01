using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Web.ShareModels;

namespace Web.CustomerSite.Services
{
    public class CategoryApiClient : ICategoryApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public CategoryApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IList<Category>> GetType()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["Domain:Default"] + "/api/v1/Category");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<Category>>();
        }
    }
}
