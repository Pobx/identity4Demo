using System;
// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace identity4Demo {
  public static class Config {
    public static List<TestUser> GetUsers () {
      return new List<TestUser> {

        new () {
          SubjectId = "1",
            Username = "pobx",
            Password = "1234"
        },

        new () {
          SubjectId = "2",
            Username = "wanwisa",
            Password = "1234"
        }
      };
    }

    public static IEnumerable<ApiResource> ApiResources =>
      new ApiResource[] {
        new () {
        Name = "ro.client.token",
        ApiSecrets = { new Secret ("pobx".Sha256 ()) },
        Scopes = { "level1", "level2" },
        // Scopes = { "level2" },
        }
      };
    public static IEnumerable<IdentityResource> IdentityResources =>
      new IdentityResource[] {
        new IdentityResources.OpenId ()
      };

    public static IEnumerable<ApiScope> ApiScopes =>
      new ApiScope[] {
        new ("level1", "เบิงได้อย่างเดียว เฮ็ดหยังบ่ได้"),
        new ("level2", "เฮ็ดได้เป็นฮะลังแนว"),
        new ("level3", "เฮ็ดได้เบิด")
      };

    public static IEnumerable<Client> Clients =>
      new Client[] {
        new () {
        ClientId = "pobx",
        AllowedGrantTypes = GrantTypes.ClientCredentials,
        ClientSecrets = {
        new Secret ("secret1234".Sha256 ())
        },
        AllowedScopes = { "level1" }
        },

        new () {
        ClientId = "ro.client.jwt",
        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
        ClientSecrets = {
        new Secret ("secret1234".Sha256 ()),
        },
        AllowedScopes = { "level1", "level2", "level3", "offline_access" },
        AllowOfflineAccess = true,
        AccessTokenLifetime = 300,
        UpdateAccessTokenClaimsOnRefresh = true,
        AbsoluteRefreshTokenLifetime = 600,
        SlidingRefreshTokenLifetime = 420,
        RefreshTokenUsage = TokenUsage.OneTimeOnly,
        AccessTokenType = AccessTokenType.Jwt
        },
        new () {
        ClientId = "ro.client.token",
        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
        ClientSecrets = {
        new Secret ("secret1234".Sha256 ()),
        },
        AllowedScopes = { "level1", "level2", "level3", "offline_access" },
        AllowOfflineAccess = true,
        AccessTokenLifetime = 300,
        UpdateAccessTokenClaimsOnRefresh = true,
        AbsoluteRefreshTokenLifetime = 600,
        SlidingRefreshTokenLifetime = 420,
        RefreshTokenUsage = TokenUsage.OneTimeOnly,
        AccessTokenType = AccessTokenType.Reference
        },

      };
  }
}