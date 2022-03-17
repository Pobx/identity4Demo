// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using IdentityServer4.Models;

namespace identity4Demo {
  public static class Config {
    public static IEnumerable<IdentityResource> IdentityResources =>
      new IdentityResource[] {
        new IdentityResources.OpenId ()
      };

    public static IEnumerable<ApiScope> ApiScopes =>
      new ApiScope[] {
        new ApiScope ("api1", "API 1")
      };

    public static IEnumerable<Client> Clients =>
      new Client[] {
        new Client {
        ClientId = "pobx",
        AllowedGrantTypes = GrantTypes.ClientCredentials,
        ClientSecrets = {
        new Secret ("secret1234".Sha256 ())
        },
        AllowedScopes = { "api1" }
        }
      };
  }
}