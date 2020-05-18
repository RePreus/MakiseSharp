using System.Collections.Generic;

namespace MakiseSharp.Application.Common.Interfaces
{
    public interface IJsonService
    {
        IEnumerable<T> DeserializeJson<T>(string content);
    }
}
