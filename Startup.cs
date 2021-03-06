using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using IdentityServer4;
// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace identity4Demo {
  public class Startup {
    public IWebHostEnvironment Environment { get; }

    public Startup (IWebHostEnvironment environment) {
      Environment = environment;
    }

    public void ConfigureServices (IServiceCollection services) {
      // uncomment, if you want to add an MVC-based UI
      //services.AddControllersWithViews();

      // string Certificate = "ssl/dev.cer";

      // Load the certificate into an X509Certificate object.
      // var cert = new X509Certificate2 (Certificate);
      var cert = new X509Certificate2 (Path.Combine (Directory.GetCurrentDirectory (), "ssl/https.pfx"), "1234");

      // Get the value.
      string resultsTrue = cert.ToString (true);

      // Display the value to the console.
      Console.WriteLine (resultsTrue);

      // Get the value.
      string resultsFalse = cert.ToString (false);

      // Display the value to the console.
      Console.WriteLine (resultsFalse);

      var builder = services.AddIdentityServer (options => {
          // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
          options.EmitStaticAudienceClaim = true;
        })
        .AddInMemoryIdentityResources (Config.IdentityResources)
        .AddInMemoryApiScopes (Config.ApiScopes)
        .AddInMemoryClients (Config.Clients)
        .AddInMemoryApiResources (Config.ApiResources)
        .AddTestUsers (Config.GetUsers ());

      // not recommended for production - you need to store your key material somewhere secure
      // builder.AddDeveloperSigningCredential();
      builder.AddSigningCredential (cert);
      // builder.AddValidationKey();

      services.AddAuthentication ()
        .AddCookie (options => {
          options.ExpireTimeSpan = TimeSpan.FromMinutes (5);
          options.SlidingExpiration = true;
        });
    }

    public void Configure (IApplicationBuilder app) {
      if (Environment.IsDevelopment ()) {
        app.UseDeveloperExceptionPage ();
      }

      // uncomment if you want to add MVC
      //app.UseStaticFiles();
      //app.UseRouting();

      app.UseIdentityServer ();

      // uncomment, if you want to add MVC
      //app.UseAuthorization();
      //app.UseEndpoints(endpoints =>
      //{
      //    endpoints.MapDefaultControllerRoute();
      //});
    }
  }
}