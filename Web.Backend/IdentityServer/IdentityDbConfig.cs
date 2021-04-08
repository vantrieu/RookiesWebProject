using System.Collections.Generic;
using System.Linq;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityDbConfig
    {
        public static void MigrateIdServerDb(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();

                var clients = new List<Client>();
                if (!context.Clients.Any(x => x.ClientId == "client"))
                {
                    clients.Add(new Client
                    {
                        ClientId = "client",
                        ClientSecrets = { new Secret("secret".Sha256()) },

                        AllowedGrantTypes = IdentityServer4.Models.GrantTypes.ClientCredentials,
                        // scopes that client has access to
                        AllowedScopes = { "rookieshop.api" }
                    });
                }

                if (!context.Clients.Any(x => x.ClientId == "mvc"))
                {
                    clients.Add(new Client
                    {
                        ClientId = "mvc",
                        ClientSecrets = { new Secret("secret".Sha256()) },

                        AllowedGrantTypes = GrantTypes.Code,

                        RedirectUris = { "https://localhost:44325/signin-oidc" },

                        PostLogoutRedirectUris = { "https://localhost:44325/signout-callback-oidc" },

                        AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "rookieshop.api"
                    },

                        AccessTokenLifetime = 90,
                        AllowOfflineAccess = true,

                    });
                }

                if (!context.Clients.Any(x => x.ClientId == "swagger"))
                {
                    clients.Add(new Client
                    {
                        ClientId = "swagger",
                        ClientSecrets = { new Secret("secret".Sha256()) },
                        AllowedGrantTypes = GrantTypes.Code,

                        RequireConsent = false,
                        RequirePkce = true,

                        RedirectUris = { $"https://localhost:44314/swagger/oauth2-redirect.html" },
                        PostLogoutRedirectUris = { $"https://localhost:44314/swagger/oauth2-redirect.html" },
                        AllowedCorsOrigins = { $"https://localhost:44314" },

                        AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "rookieshop.api"
                    }
                    });
                }

                if (clients.Any())
                {
                    context.Clients.AddRange(clients.Select(x => x.ToEntity()));
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    var identityResources = new List<IdentityResource>()
                    {
                        new IdentityResources.OpenId(),
                        new IdentityResources.Profile(),
                    };

                    context.IdentityResources.AddRange(identityResources.Select(x => x.ToEntity()));

                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    var apiScopes = new List<ApiScope>()
                    {
                         new ApiScope("rookieshop.api", "Rookie Shop API"),
                    };

                    context.ApiScopes.AddRange(apiScopes.Select(x => x.ToEntity()));
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    var apiResources = new List<ApiResource>
                    {
                        new ApiResource("rookieshop.api", "Rookie Shop API", new[] { "role" })
                        {
                            Scopes = { "rookieshop.api" },
                        },
                    };

                    context.ApiResources.AddRange(apiResources.Select(x => x.ToEntity()));

                    context.SaveChanges();
                }
            }
        }
    }
}
