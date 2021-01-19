using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
            #region For host on linux use
               //.UseKestrel(options =>
               // {
               //     options.AddServerHeader = false;
               //     options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(3);
               //     options.Limits.MaxRequestBodySize = 20 * 1024;
               //     options.Limits.MinRequestBodyDataRate = new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
               //     options.Limits.MinResponseDataRate = new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
               //     options.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(60);
               // })
            #endregion 
               .ConfigureLogging((hostContext, logging) =>
               {
                   var env = hostContext.HostingEnvironment;
                   var configuration = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile($"appsettings.json", false, true)
                       .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                       .Build();
                   logging.AddConfiguration(configuration.GetSection("Logging"));
               })
               .UseNLog()
               .UseStartup<Startup>();
        }
    }
}
