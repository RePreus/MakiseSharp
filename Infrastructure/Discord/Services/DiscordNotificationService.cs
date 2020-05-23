using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using MakiseSharp.Application.Common.Interfaces;
using MakiseSharp.Domain.Models;
using MakiseSharp.Infrastructure.Discord.Common;

namespace MakiseSharp.Infrastructure.Discord.Services
{
    public class DiscordNotificationService : IDiscordNotificationService
    {
        private readonly DiscordConfiguration configuration;
        private readonly DiscordSocketClient client;
        public DiscordNotificationService(DiscordConfiguration configuration, DiscordSocketClient client)
        {
            this.configuration = configuration;
            this.client = client;
        }
        public async Task SendNotification(BuildDetails buildDetails)
        {
            await client.GetGuild(configuration.GuildId)
                .GetTextChannel(configuration.ChannelId)
                .SendMessageAsync(string.Empty, embed: BuildEmbedMessage(buildDetails));
        }

        private Embed BuildEmbedMessage(BuildDetails buildDetails)
        {
            var eAuthor = new EmbedAuthorBuilder
            {
                Name = buildDetails.ProjectDetails.ProjectName,
                Url = "https://github.com/" + buildDetails.RepositoryDetails.Id
            };
            var eFooter = new EmbedFooterBuilder {Text = "Azure Pipelines"};

            var color = buildDetails.Result == "succeeded" ? Colors.Green : Colors.Red;

            var e = new EmbedBuilder
            {
                Author = eAuthor,
                Footer = eFooter,
                Color = color,
                Title = $"Build: #{buildDetails.Id} on {buildDetails.PipelineDetails.PipelineName} {buildDetails.Result}",
                Url = buildDetails.Url
            };
            var field = new EmbedFieldBuilder
            {
                Name = $"[{buildDetails.RepositoryDetails.Id}:{buildDetails.TriggerDetails.SourceBranch ?? string.Empty}]",
                Value = $"{buildDetails.TriggerDetails.Message ?? string.Empty}"
            };
            e.AddField(field);
            return e.Build();
        }
    }
}
