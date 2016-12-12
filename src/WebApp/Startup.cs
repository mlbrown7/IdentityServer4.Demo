using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //When adding middleware components to the owin pipeline, order matters.  Authentiaction components need to be configure before MVC
            ConfigureAuthentication(app);
            ConfigureMVC(app);
        }

        private void ConfigureAuthentication(IApplicationBuilder app)
        {
            //asp.net core cookie authentication stores the user principle in an encrypted cookie
            //the cookie is validated on each request
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "cookies"        //name of the OWIN middleware
            });

            //this keeps Microsoft.Identity from re-formatting claims and adding namespaces which make it hard to use
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //use openid authentication
            //default scope asked for is profile
            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                AuthenticationScheme = "oidc",
                SignInScheme = "cookies",
                Authority = "http://localhost:12345",    //url to identity server
                RequireHttpsMetadata = false,
                ClientId = "webapp",
                SaveTokens = true,
                ResponseType = "id_token",
                Scope = { "openid profile email" }
            });
        }

        private void ConfigureMVC(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
