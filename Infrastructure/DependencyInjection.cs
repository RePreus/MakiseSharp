using Discord;
using Discord.WebSocket;
using GreenPipes;
using MakiseSharp.Application.Common.Interfaces;
using MakiseSharp.Application.Common.Services;
using MakiseSharp.Application.Consumers;
using MakiseSharp.Domain.Models;
using MakiseSharp.Infrastructure.Discord;
using MakiseSharp.Infrastructure.Discord.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MakiseSharp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDiscord(this IServiceCollection services, IConfiguration configuration)
        {
            var discordConfig = new DiscordConfiguration();
            configuration.GetSection("DiscordSettings").Bind(discordConfig);
            services.AddSingleton(discordConfig);

            var botConfig = new BotConfiguration();
            configuration.GetSection("BotSettings").Bind(botConfig);

            var client = new DiscordSocketClient();
            client.LoginAsync(TokenType.Bot, botConfig.Token).Wait();
            client.StartAsync().Wait();
            services.AddSingleton(client);

            services.AddScoped<IDiscordNotificationService, DiscordNotificationService>();

            return services;
        }

        public static IServiceCollection AddMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<BuildDetailsConsumer>();
            });

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost");

                cfg.SetLoggerFactory(provider.GetService<ILoggerFactory>());

                cfg.ReceiveEndpoint("Discord", e =>
                {
                    e.Consumer<BuildDetailsConsumer>(provider);

                    EndpointConvention.Map<BuildDetails>(e.InputAddress);
                });
            }));

            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

            services.AddSingleton<IHostedService, BusService>();
            //services.AddScoped(provider => provider.GetRequiredService<IBus>());

            return services;
        }
    }
}
