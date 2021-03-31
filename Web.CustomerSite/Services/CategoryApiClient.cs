using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Web.ShareModels;

namespace Web.CustomerSite.Services
{
    public class CategoryApiClient : ICategoryApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<Category>> GetType()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:44314/api/v1/Category");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<Category>>();
        }
    }
}
