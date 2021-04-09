using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Web.CustomerSite.Services
{
    public interface IRequestServices
    {
        HttpClient CreateRequest();

        Task<HttpClient> CreateRequestWithAuth();
    }
}
