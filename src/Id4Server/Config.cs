// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Id4Server
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("api12","My API"),
                new ApiResource("mgxserver", "mgx game server")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client{
                    ClientId = "client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets = {
                        new Secret("mysecret".Sha256())
                    },
                    AllowedScopes = {"api12"}
                },
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = {new Secret("mxsecret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    AllowedScopes = 
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api12"
                    },
                    AllowOfflineAccess = true,
                },
                new Client
                {
                    ClientId = "js",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris = {"http://localhost:5003/callback.html"},
                    PostLogoutRedirectUris = {"http://localhost:5003/index.html"},
                    AllowedCorsOrigins = {"http://localhost:5003"},
                    AllowedScopes = 
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api12"
                    },
                },
                new Client
                {
                    ClientId = "mgxhtml5client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris = {"http://localhost:5550/callback.html"},
                    PostLogoutRedirectUris = {"http://localhost:5550/index.html"},
                    AllowedCorsOrigins = {"http://localhost:5550"},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "mgxserver"
                    },
                }
             };

    }
}