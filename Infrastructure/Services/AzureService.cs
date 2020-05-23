using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MakiseSharp.Application.Common.Configuration;
using MakiseSharp.Application.Common.Interfaces;
using MakiseSharp.Domain.Models;

namespace MakiseSharp.Infrastructure.Services
{
    public class AzureService : IAzureService
    {
        private readonly HttpClient client;
        private readonly List<AzureConfiguration> configs;
        private readonly IJsonService jsonService;
        public AzureService(HttpClient client, List<AzureConfiguration> configs, IJsonService jsonService)
        {
            this.client = client;
            this.configs = configs;
            this.jsonService = jsonService;
        }

        public async Task<IEnumerable<BuildDetails>> GetRecentBuildsDetails()
        {
            var builds = new List<BuildDetails>();
            foreach (var config in configs)
            {
                var path = config.GetRecentBuilds(DateTime.Today.AddSeconds(-30));
                var response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    var buildDetails = jsonService.DeserializeJson<BuildDetails>(await response.Content.ReadAsStringAsync());
                    builds.AddRange(buildDetails);
                }
            }

            return builds;
        }
    }
}
