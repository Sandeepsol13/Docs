﻿using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Server.HttpSys;

// The default listening address is http://localhost:5000 if none is specified.
// Replace "localhost" with "*" to listen to external requests.
// You can use the --urls command-line flag to change the listening address. Ex:
// > dotnet run --urls http://*:8080;http://*:8081

// Use the following code to configure URLs in code:
// builder.UseUrls("http://*:8080", "http://*:8081");
// Put it after UseConfiguration(config) to take precedence over command-line configuration.

namespace HttpSysDemo
{
    /// <summary>
    /// Executing the "dotnet run" command in the application folder will run this app.
    /// </summary>
    public class Program
    {
        public static string Server;

        #region snippet_Main
        public static int Main(string[] args)
        {
            Console.WriteLine("Running demo with HttpSys.");

            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var builder = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .UseHttpSys(options =>
                {
                    options.Authentication.Schemes = AuthenticationSchemes.None;
                    options.Authentication.AllowAnonymous = true;
                    options.UrlPrefixes.Add("http://localhost:5000");
                });

            var host = builder.Build();
            host.Run();

            return 0;
        }
        #endregion
    }
}
