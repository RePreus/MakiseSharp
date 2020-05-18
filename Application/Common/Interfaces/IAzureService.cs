using System.Collections.Generic;
using System.Threading.Tasks;
using MakiseSharp.Domain.Models;

namespace MakiseSharp.Application.Common.Interfaces
{
    public interface IAzureService
    {
        Task<IEnumerable<BuildDetails>> GetRecentBuildsDetails();
    }
}
