using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Web.ShareModels;

namespace Web.CustomerSite.Services
{
    public class CategoryApiClient : ICategoryApiClient
    {
        private readonly IRequestServices _requestServices;

        public CategoryApiClient(IRequestServices requestServices)
        {
            _requestServices = requestServices;
        }

        public async Task<IList<Category>> GetCategory()
        {
            var client = _requestServices.CreateRequest();
            var response = await client.GetAsync("/api/v1/Category");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<Category>>();
        }
    }
}
