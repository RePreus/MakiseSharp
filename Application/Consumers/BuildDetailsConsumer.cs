using System.Threading.Tasks;
using MakiseSharp.Application.Common.Interfaces;
using MakiseSharp.Domain.Models;
using MassTransit;

namespace MakiseSharp.Application.Consumers
{
    public class BuildDetailsConsumer : IConsumer<BuildDetails>
    {
        private readonly IDiscordNotificationService notificationService;

        public BuildDetailsConsumer(IDiscordNotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        public async Task Consume(ConsumeContext<BuildDetails> context)
        {
            await notificationService.SendNotification(context.Message);
        }
    }
}
