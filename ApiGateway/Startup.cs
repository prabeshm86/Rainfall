using System;
using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
//using HealthChecks.UI.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ApiGateway
{
    public class Startup
    {
        private readonly IConfiguration _cfg;
        public Startup(IConfiguration configuration)
        {
            this._cfg = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var identityUrl = _cfg.GetValue<string>("IdentityUrl");
            var authenticationProviderKey = "IdentityApiKey";

            // services.AddHealthChecks()
            //     .AddCheck("self", () => HealthCheckResult.Healthy())
            //     .AddUrlGroup(new Uri(_cfg["ProductUrlHC"]), name: "productapi-check", tags: new string[] { "productapi" });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials());
            });

            services.AddAuthentication()
                .AddJwtBearer(authenticationProviderKey, x =>
                {
                    x.Authority = identityUrl;
                    x.RequireHttpsMetadata = false;
                    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidAudiences = new[] { "orders", "basket", "locations", "marketing", "mobileshoppingagg", "webshoppingagg" }
                    };
                    x.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents()
                    {
                        OnAuthenticationFailed = async ctx =>
                        {
                            int i = 0;
                        },
                        OnTokenValidated = async ctx =>
                        {
                            int i = 0;
                        },

                        OnMessageReceived = async ctx =>
                        {
                            int i = 0;
                        }
                    };
                });
            
            services.AddOcelot(_cfg);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var pathBase = _cfg["PATH_BASE"];

            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
            
}
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHealthChecks("/hc", new HealthCheckOptions()
            // {
            //     Predicate = _ => true,
            //     ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            // });

            // app.UseHealthChecks("/liveness", new HealthCheckOptions
            // {
            //     Predicate = r => r.Name.Contains("self")
            // });

            app.UseCors("CorsPolicy");

            app.UseOcelot().Wait();
        }
    }
}
