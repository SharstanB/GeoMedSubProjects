
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
namespace GMIdentityServer
{
    public class Config
    {
        //public static IEnumerable<IdentityResource> GetIdentityResources()
        //{
        //    return new List<IdentityResource>
        //    {
        //        new IdentityResources.OpenId(),
        //        new IdentityResources.Profile(),
        //    };
        //}



        //public static IEnumerable<Client> GetClients()
        //{
        //    return new List<Client>
        //    {
        //        // other clients omitted...

        //        // OpenID Connect implicit flow client (MVC)
        //        //new Client
        //        //{
        //        //    //AllowedGrantTypes = GrantTypes.ClientCredentials,
        //        //    ClientSecrets = new List<Secret> {new Secret("sdfghjkhgfdfghjklkjhgfdfghj".Sha256())}, // change me!
        //        //    //AllowedScopes = new List<string> {"api1.read"},
        //        //    ClientId = "dash",
        //        //    ClientName = "MVC Client",
        //        //    AllowedGrantTypes = GrantTypes.Implicit,

        //        //    // where to redirect to after login
        //        //    RedirectUris = { "http://localhost:5002/signin-oidc" },

        //        //    // where to redirect to after logout
        //        //    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

        //        //    AllowedScopes = new List<string>
        //        //    {
        //        //        IdentityServerConstants.StandardScopes.OpenId,
        //        //        IdentityServerConstants.StandardScopes.Profile
        //        //    }
        //        //},
        //        // new Client
        //        //{
        //        //    //AllowedGrantTypes = GrantTypes.ClientCredentials,
        //        //    ClientSecrets = new List<Secret> {new Secret("qawsedrftgyhujioiuhytrdesdfghjkkl".Sha256())}, // change me!
        //        //    //AllowedScopes = new List<string> {"api1.read"},
        //        //    ClientId = "api",
        //        //    ClientName = "api Client",
        //        //    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,

        //        //    // where to redirect to after login
        //        //    RedirectUris = { "http://localhost:5002/signin-oidc" },

        //        //    // where to redirect to after logout
        //        //    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

        //        //    AllowedScopes = new List<string>
        //        //    {
        //        //        IdentityServerConstants.StandardScopes.OpenId,
        //        //        IdentityServerConstants.StandardScopes.Profile,
        //        //        IdentityServerConstants.StandardScopes.Email,
        //        //    }
        //        //}
        //         new Client
        //        {
        //            ClientId = "oauthClient",
        //            ClientName = "Example client application using client credentials",
        //            AllowedGrantTypes = GrantTypes.ClientCredentials,
        //            ClientSecrets = new List<Secret> {new Secret("SuperSecretPassword".Sha256())}, // change me!
        //            AllowedScopes = new List<string> {"api1.read"}
        //        }

        //    };
        //}

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
            new ApiScope("apiscope", "Access to API"),
            new ApiScope("dashscope", "Access to Dashboard"),
        };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("apiResourse", "API Resource")
                {
                    Scopes =  { 
                    IdentityServerConstants.StandardScopes.OpenId,
                    ////IdentityServerConstants.StandardScopes.Email,
                    //IdentityServerConstants.StandardScopes.Profile
                    } ,
                }
                ,
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                // for public api
                new Client
                {
                    ClientId = "secret_dash_client_id",

                    AllowedGrantTypes = GrantTypes.Code,
                    
                    RequirePkce = true,

                    ClientSecrets =
                    {
                        new Secret("SecretGeoMedDashBoardProject2021".Sha256())
                    },

                    ClientUri = "https://localhost:44363",
                    
                    RedirectUris = { "https://localhost:44363/signin-oidc" },

                    PostLogoutRedirectUris = { "https://localhost:44363/signout-callback-oidc" },

                    AllowedScopes = {
                        "dashscope",
                        IdentityServerConstants.StandardScopes.OpenId ,
                    }
                },
                 new Client
                {
                    ClientId = "secret_api_client_id",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("SecretGeoMedApi2021".Sha256())
                    },
                    ClientUri = "https://localhost:44335",
                    AllowedScopes = {
                         "apiscope",
                         IdentityServerConstants.StandardScopes.OpenId, }
                }
            };
        }

      

    }
}
