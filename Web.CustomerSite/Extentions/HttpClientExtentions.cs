using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Web.CustomerSite.Extentions
{
    public static class HttpClientExtentions
    {
        public static void UseBasicAuthentication(this HttpClient client, string userName, string password)
        {
            var byteArray = Encoding.UTF8.GetBytes(userName + ":" + password);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public static void UseBearerToken(this HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
