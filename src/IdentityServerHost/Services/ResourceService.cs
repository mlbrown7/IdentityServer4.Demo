using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IdentityServer4.Models;

namespace IdentityServerHost.Services
{
    public class ResourceService
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("roles", new List<string> { "role" })
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "webapi",
                    DisplayName = "WebAPI Resource",
                    Scopes = new List<Scope>
                    {
                        new Scope("foo"),
                        new Scope("users")
                    }
                }
            };
        }
    }
}
