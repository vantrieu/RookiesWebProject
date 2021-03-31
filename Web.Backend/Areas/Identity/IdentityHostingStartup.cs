using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Web.Backend.Areas.Identity.IdentityHostingStartup))]
namespace Web.Backend.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}