using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.CustomerSite.Extentions;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;

namespace Web.CustomerSite.Services
{
    public class OrderApiClient : IOrderApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenServices _tokenServices;

        public OrderApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, 
            IHttpContextAccessor httpContextAccessor, ITokenServices tokenServices)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _tokenServices = tokenServices;
        }

        public async Task<bool> PostOrderAsync(List<int> productIds)
        {
            var client = _httpClientFactory.CreateClient();
            var accessToken = await _tokenServices.RefreshTokenAsync();
            client.UseBearerToken(accessToken);
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(productIds),
                Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_configuration["Domain:Default"] + "/api/v1/Order", httpContent);
            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
