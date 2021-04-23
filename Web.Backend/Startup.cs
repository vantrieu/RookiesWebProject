using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using Web.Backend.IdentityServer;
using Web.Backend.Services;
using Web.Services;
using Web.Services.Interfaces;
using Web.Services.Repositories;
using Web.ShareModels;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;

namespace Web.Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IFileImageRepository, FileImageRepository>();
            services.AddTransient<IProductFileImageRepository, ProductFileImageRepository>();
            services.AddTransient<IFileServices, FileServices>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderDetailrepository, OrderDetailrepository>();
            services.AddTransient<IRateRepository, RateRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddDefaultIdentity<User>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(365);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            })
               //.AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
               //.AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
               //.AddInMemoryClients(IdentityServerConfig.Clients)
               //.AddInMemoryPersistedGrants()
               .AddAspNetIdentity<User>()
               .AddProfileService<CustomProfileService>()
               .AddDeveloperSigningCredential() // not recommended for production - you need to store your key material somewhere secure
               .AddConfigurationStore(options =>
               {
                   options.ConfigureDbContext = builder =>
                       builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                           sql =>
                           {
                               if (!string.IsNullOrEmpty(typeof(Startup).GetTypeInfo().Assembly.GetName().Name))
                               {
                                   sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                               }
                           });
                   options.DefaultSchema = "idsv";
               })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                            sql =>
                            {
                                if (!string.IsNullOrEmpty(typeof(Startup).GetTypeInfo().Assembly.GetName().Name))
                                {
                                    sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                }
                            });
                    options.DefaultSchema = "idsv";

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30; // interval in seconds
                });

            services.AddAuthentication()
                .AddLocalApi("Bearer", option =>
                {
                    option.ExpectedScope = "rookieshop.api";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Bearer", policy =>
                {
                    policy.AddAuthenticationSchemes("Bearer");
                    policy.RequireAuthenticatedUser();
                });
            });

            services.AddControllersWithViews();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rookie Shop API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri("/connect/token", UriKind.Relative),
                            AuthorizationUrl = new Uri("/connect/authorize", UriKind.Relative),
                            Scopes = new Dictionary<string, string> { { "rookieshop.api", "Rookie Shop API" } }
                        },
                    },
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>{ "rookieshop.api" }
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.MigrateIdServerDb(Configuration);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("AllowAnyOrigin");

            //app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.OAuthClientId("swagger");
                c.OAuthClientSecret("secret");
                c.OAuthUsePkce();
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rookie Shop API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
