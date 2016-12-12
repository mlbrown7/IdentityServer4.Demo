using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

using IdentityServer4.Services.InMemory;

namespace IdentityServerHost.Services
{
    public class UserService
    {
        public static IEnumerable<InMemoryUser> GetUsers()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "bob@demo.com",
                    Username = "bob@demo.com",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim(IdentityModel.JwtClaimTypes.GivenName, "bob"),
                        new Claim(IdentityModel.JwtClaimTypes.FamilyName, "smith"),
                        new Claim(IdentityModel.JwtClaimTypes.Email, "bob@demo.com")
                    }
                },
                new InMemoryUser
                {
                    Subject = "jane@demo.com",
                    Username = "jane@demo.com",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim(IdentityModel.JwtClaimTypes.GivenName, "jane"),
                        new Claim(IdentityModel.JwtClaimTypes.FamilyName, "doe"),
                        new Claim(IdentityModel.JwtClaimTypes.Email, "jane@demo.com")
                    }
                }
            };
        }
    }
}
