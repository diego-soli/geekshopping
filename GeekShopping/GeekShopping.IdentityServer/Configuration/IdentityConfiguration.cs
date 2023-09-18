using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.IdentityServer.Configuration
{
    public static class IdentityConfiguration
    {
        public const string Admin = "Admin";
        public const string Client = "Client";

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource> 
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
        { 
            new ApiScope("geek_shopping","GeekShopping Server"),
            new ApiScope(name: "read","Read data."),
            new ApiScope(name: "write","Write data."),
            new ApiScope(name: "delete","Delete data."),
        };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("my_super_secret".Sha256()) }, //string encriptada //hardcoded mudar para pegar pela appsettings, problema de seguranca se nao mudar.
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "read", "write", "profile" }
            },            
            new Client
            {
                ClientId = "geek_shopping",
                ClientSecrets = { new Secret("my_super_secret".Sha256()) }, //string encriptada //hardcoded mudar para pegar pela appsettings, problema de seguranca se nao mudar.
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = {"https://localhost:4430/signin-oidc"},
                PostLogoutRedirectUris = {"https://localhost:4430/signout-callback-oidc"},
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "geek_shopping"
                }
            }

        };
        

    }
}
