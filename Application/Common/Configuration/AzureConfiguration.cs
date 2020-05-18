using System;

namespace MakiseSharp.Application.Common.Configuration
{
    public class AzureConfiguration
    {
        public string Organization { get; set; }

        public string Project { get; set; }

        private const string scheme = "https://";
        private const string hostName = "dev.azure.com/";
        private const string path = "/_apis/build/builds";
        private const string query = "?api-version=5.1";
        private const string minTimeQuery = "&minTime=";

        public string GetRecentBuilds(DateTime minTime)
            => scheme + hostName + Organization + "/" + Project + path + query + minTimeQuery +
               minTime;
    }
}
