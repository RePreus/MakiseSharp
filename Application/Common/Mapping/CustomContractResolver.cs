using System.Collections.Generic;
using Newtonsoft.Json.Serialization;

namespace MakiseSharp.Application.Common.Mapping
{
    public class CustomContractResolver : DefaultContractResolver
    {
        private Dictionary<string, string> PropertyMappings { get; }

        public CustomContractResolver()
        {
            PropertyMappings = new Dictionary<string, string>
            {
                {"AuthorDetails", "requestedFor"},
                {"PipelineDetails", "definition"},
                {"RepositoryDetails", "repository"},
                {"ProjectDetails", "project"},
                {"Name", "displayName"},
                {"TriggerDetails", "triggerInfo"},
                {"Message", "ci.message"},
                {"SourceBranch", "ci.sourceBranch"},
                {"ProjectName", "name"},
                {"PipelineName", "name"}
            };
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            var resolved = PropertyMappings.TryGetValue(propertyName, out var resolvedName);
            return (resolved) ? resolvedName : base.ResolvePropertyName(propertyName);
        }
    }
}
