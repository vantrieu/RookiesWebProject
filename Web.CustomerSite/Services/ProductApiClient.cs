using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Web.ShareModels;
using Web.ShareModels.ViewModels;

namespace Web.CustomerSite.Services
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly IRequestServices _requestServices;

        public ProductApiClient(IRequestServices requestServices)
        {
            _requestServices = requestServices;
        }
        public async Task<IList<ProductVm>> GetProduct()
        {
            var client = _requestServices.CreateRequest();
            var response = await client.GetAsync("/api/v1/Product");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<ProductVm>>();
        }

        public async Task<ProductVm> GetProductById(int id)
        {
            var client = _requestServices.CreateRequest();
            var response = await client.GetAsync("/api/v1/Product/" + id);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<ProductVm>();
        }

        public async Task<IList<ProductVm>> GetProductByCategory(string categoryName)
        {
            var client = _requestServices.CreateRequest();
            var response = await client.GetAsync("/api/v1/Product/category=" + categoryName);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<ProductVm>>();
        }

        public async Task<IList<ProductVm>> GetProductByArray(List<int> temp)
        {
            List<ProductVm> lstProduct = new List<ProductVm>();
            var client = _requestServices.CreateRequest();
            if (temp == null)
                return lstProduct;
            foreach (int id in temp)
            {
                var response = await client.GetAsync("/api/v1/Product/" + id);
                response.EnsureSuccessStatusCode();
                lstProduct.Add(await response.Content.ReadAsAsync<ProductVm>());
            }
            return lstProduct;
        }

        public async Task<Rate> PostRating(int id, int rank)
        {
            var client = await _requestServices.CreateRequestWithAuth();
            var response = await client.PostAsync("/api/v1/Product/rate?productId=" + id + "&totalStar=" + rank, null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<Rate>();

        }

        public async Task<ProductVm> GetProductForRating(int id)
        {
            var client = await _requestServices.CreateRequestWithAuth();
            var response = await client.GetAsync("/api/v1/Product/rate/" + id);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<ProductVm>();
        }
    }

}
