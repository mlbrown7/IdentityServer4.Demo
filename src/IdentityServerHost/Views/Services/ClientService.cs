using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IdentityServer4.Models;

namespace IdentityServerHost.Services
{
    public class ClientService
    {
        public static IEnumerable<IdentityServer4.Models.Client> GetClients()
        {
            List<IdentityServer4.Models.Client> clients = new List<IdentityServer4.Models.Client>();
            clients.Add(new IdentityServer4.Models.Client
            {
                ClientId = "webapp",
                ClientName = "Demo WebApp",
                AllowedGrantTypes = GrantTypes.Implicit,
                ClientSecrets = { new Secret("password".Sha256())  },
                AllowedScopes = new List<string> { IdentityServer4.IdentityServerConstants.StandardScopes.OpenId, IdentityServer4.IdentityServerConstants.StandardScopes.Profile, IdentityServer4.IdentityServerConstants.StandardScopes.Email },
                RedirectUris = { "http://localhost:62021/signin-oidc" },
                PostLogoutRedirectUris = { "http://localhost:62021" },
            });
            return clients;
        }
    }
}
