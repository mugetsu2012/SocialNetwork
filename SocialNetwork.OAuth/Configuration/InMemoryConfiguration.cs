using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace SocialNetwork.OAuth.Configuration
{
    /// <summary>
    /// Configuration for the InMemory model
    /// </summary>
    public class InMemoryConfiguration
    {
        public static IEnumerable<ApiResource> ApiResources()
        {
            return new[]
            {
                new ApiResource("socialnetwork", "Social Network", new List<string>()
                {
                    ClaimTypes.Role,
                    "cuenta",
                    ClaimTypes.Surname
                }),
                new ApiResource("coffeshop", "The Great Coffe Shop")
            };
        }

        public static IEnumerable<IdentityResource> IdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<Client> Clients()
        {
            return new[]
            {
                new Client()
                {
                    ClientId = "socialnetwork",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new []{"socialnetwork" },
                    Claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Role,"SocialNetwork.Api"),
                        new Claim(ClaimTypes.Surname, "SocialNetworks")
                    }
                },
                new Client()
                {
                    ClientId = "coffeshop",
                    ClientSecrets = new [] { new Secret("coffe123$".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] {"coffeshop"}
                },
                new Client()
                {
                    ClientId = "socialnetwork_implicit",
                    ClientSecrets = new []{new Secret("secret".Sha256()), },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = new []
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "socialnetwork",
                    },
                    RedirectUris = new [] { "http://localhost:63686/signin-oidc" },
                    PostLogoutRedirectUris = new [] { "http://localhost:63686/signout-callback-oidc" }
                }, 

            };
        }

        public static IEnumerable<TestUser> Users()
        {
            return new[]
            {
                new TestUser()
                {
                    SubjectId = "1",
                    Username = "dortiz",
                    Password = "pass123",
                    Claims = new List<Claim>()
                    {
                        new Claim("cuenta", "12346"),
                        new Claim(ClaimTypes.Role, "SR.Domiciolio.Web.Controllers.Login"),
                        new Claim(ClaimTypes.Role, "SR.Domiciolio.Web.Controllers.Inicio")
                    }
                },
            };
        }
    }
}
