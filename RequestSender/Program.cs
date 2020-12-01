using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Web;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace RequestSender
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{envName}.json")
                .AddJsonFile($"appsettings.json")
                .Build();
            NLog.Extensions.Logging.ConfigSettingLayoutRenderer.DefaultConfiguration = config;
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseNLog()
                .ConfigureLogging(logger =>
                {
                    logger.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
        }
    }
}
