using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.CustomerSite.Services
{
    public interface ITokenServices
    {
        Task<string> GetAccessTokenAsync();

        Task<string> RefreshTokenAsync();
    }
}
