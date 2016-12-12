using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using IdentityServerHost.Services;

namespace IdentityServerHost
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            //registers IdentityServer DLL.  Uses default in-memory store
            services.AddIdentityServer()
                .AddTemporarySigningCredential()       //creates/uses temporary key to sign tokens
                .AddInMemoryClients(ClientService.GetClients())
                .AddInMemoryIdentityResources(ResourceService.GetResources())
                .AddInMemoryUsers(UserService.GetUsers().ToList());

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDeveloperExceptionPage();

            app.UseIdentityServer();

            //owin cookie middleware is used to temporarily store the results from external authentication providers
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme,
                AutomaticAuthenticate = false,
                AutomaticChallenge = false
            });

            //configure external authentication providers
            app.UseGoogleAuthentication(new GoogleOptions
            {
                AuthenticationScheme = "google",
                DisplayName = "Google Demo",
                SignInScheme = IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme,
                ClientId = "617284524847-bu3gkvv92vcubos694h86or59af16g81.apps.googleusercontent.com",
                ClientSecret = "SexwMjWDUTV4DmmQ_tEc17RK"
            });

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
