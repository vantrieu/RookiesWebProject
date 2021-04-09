using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Web.ShareModels.ViewModels;

namespace Web.CustomerSite.Services
{
    public class OrderApiClient : IOrderApiClient
    {
        private readonly IRequestServices _requestServices;

        public OrderApiClient(IRequestServices requestServices)
        {
            _requestServices = requestServices;
        }

        public async Task<bool> DeleteOrderItem(int productId, int orderId)
        {
            var client = await _requestServices.CreateRequestWithAuth();
            var response = await client.DeleteAsync("/api/v1/Order?orderId=" + orderId + "&productId=" + productId);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<bool>();
        }

        public async Task<IList<OrderVm>> GetMyOrder()
        {
            var client = await _requestServices.CreateRequestWithAuth();
            var response = await client.GetAsync("/api/v1/Order");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<OrderVm>>();
        }

        public async Task<bool> PostOrderAsync(List<int> productIds)
        {
            var client = await _requestServices.CreateRequestWithAuth();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(productIds),
                Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/v1/Order", httpContent);
            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
