using System.Collections.Generic;
using System.Linq;
using MakiseSharp.Application.Common.Interfaces;
using MakiseSharp.Application.Common.Mapping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MakiseSharp.Application.Common.Services
{
    public class JsonService : IJsonService
    {
        public IEnumerable<T> DeserializeJson<T>(string content)
        {
            var json = JObject.Parse(content);
            IList<JToken> results = json["value"].Children().ToList();
            var buildsDetails = new List<T>();
            var setting = new JsonSerializerSettings
            {
                ContractResolver = new CustomContractResolver()
            };
            foreach (var result in results)
            {
                buildsDetails.Add(JsonConvert.DeserializeObject<T>(result.ToString(), setting));
                // buildsDetails.Add(result.ToObject<T>( ));
            }
            return buildsDetails;
        }
    }
}
