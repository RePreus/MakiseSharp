using System.Collections.Generic;
using System.Globalization;
using MakiseSharp.Application.Common.Configuration;
using MakiseSharp.Application.Common.Interfaces;
using MakiseSharp.Application.Common.Services;
using MakiseSharp.Application.WebJobs;
using MakiseSharp.Presentation.Services;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MakiseSharp.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddJsonFile("./appsettings.json", optional: false);
                })
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(context.Configuration, services);
                })
                .ConfigureWebJobs(b =>
                {
                    b.AddAzureStorageCoreServices();
                    b.AddTimers();
                })
                .ConfigureLogging(b =>
                {
                    b.AddConsole();
                });

        private static void ConfigureServices(IConfiguration configuration,
            IServiceCollection services)
        {
            var azure = new List<AzureConfiguration>();
            configuration.GetSection("AzureSettings").Bind(azure);
            services.AddSingleton(azure);
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-us");

            services.AddScoped<IJsonService, JsonService>();
            services.AddSingleton<IJobActivator, JobActivator>();
            services.AddScoped<Functions>();
            services.AddDiscord(configuration);
            services.AddMassTransit();

            services.AddHttpClient<IAzureService, AzureService>();
            //services.AddHostedService<DiscordService>();
        }
    }
}
