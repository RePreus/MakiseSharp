using System;
using MakiseSharp.Application.Common.Interfaces;
using MakiseSharp.Domain.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs;

namespace MakiseSharp.Application.WebJobs
{
    public class Functions
    {
        private readonly IAzureService service;
        private readonly IBus bus;

        public Functions(IAzureService service, IBus bus)
        {
            this.service = service;
            this.bus = bus;
        }
        public async void ConsoleMessage([TimerTrigger("*/30 * * * * *")] TimerInfo myTimer, ILogger logger)
        {
            var builds = await service.GetRecentBuildsDetails();
            foreach (var build in builds)
            {
                logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(build));
                await bus.Send(build);
            }
        }
    }
}
