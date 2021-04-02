using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Web.CustomerSite.Models;

namespace Web.CustomerSite.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;

        public TokenServices(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> GetAccessTokenAsync()
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            return accessToken;
        }

        public async Task<string> RefreshTokenAsync()
        {
            var token = new TokenModel
            {
                AccessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken),
                RefreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken),
                ExpiresAt = DateTimeOffset.Parse(await _httpContextAccessor.HttpContext.GetTokenAsync("expires_at")),
            };


            if (token.TokenExpired)
            {
                var httpClient = _httpClientFactory.CreateClient();
                var metaDataResponse = await httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
                {
                    Address = "https://localhost:44314",
                    Policy = { RequireHttps = false },
                });

                var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
                var tokenResponse = await httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest
                {
                    Address = metaDataResponse.TokenEndpoint,
                    ClientId = "mvc",
                    ClientSecret = "secret",
                    RefreshToken = refreshToken,
                });

                if (tokenResponse.IsError)
                {

                }
                var auth = await _httpContextAccessor.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                auth.Properties.UpdateTokenValue(OpenIdConnectParameterNames.AccessToken, tokenResponse.AccessToken);
                auth.Properties.UpdateTokenValue(OpenIdConnectParameterNames.RefreshToken, tokenResponse.RefreshToken);
                var expiresAt = DateTime.UtcNow + TimeSpan.FromSeconds(tokenResponse.ExpiresIn);
                auth.Properties.UpdateTokenValue("expires_at", expiresAt.ToString("o", CultureInfo.InvariantCulture));
                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, auth.Principal, auth.Properties);
            }
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            return accessToken;
        }
    }
}
