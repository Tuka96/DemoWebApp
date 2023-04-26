using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

                CreateHostBuilder(args).Build().Run();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, builder) =>
            {
                //    builder.Sources.Clear();

                //    if (hostingContext.HostingEnvironment.IsDevelopment()) {
                //        builder.AddUserSecrets<Program>();
                //    }
                //    builder.AddAzureKeyVault("https://webapp1-keyvault.vault.azure.net/");
                var settings = builder.Build();
                var keyVaultURL = settings["KeyVaultConfiguration:KeyVaultURL"];
                var keyVaultClientId = settings["KeyVaultConfiguration:ClientId"];
                var keyVaultClientSecret = settings["KeyVaultConfiguration:ClientSecret"];
                builder.AddAzureKeyVault(keyVaultURL, keyVaultClientId, keyVaultClientSecret, new DefaultKeyVaultSecretManager());
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                string key = hostingContext.Configuration
                                           .GetSection("ApplicationInsights:InstrumentationKey")
                                           .Value;
                if (!String.IsNullOrEmpty(key))
                {
                    logging.AddApplicationInsights(key);
                    logging.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Error);
                    logging.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Warning);
                }
            })
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
