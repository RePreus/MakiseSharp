using System.Threading.Tasks;
using MakiseSharp.Domain.Models;

namespace MakiseSharp.Application.Common.Interfaces
{
    public interface IDiscordNotificationService
    {
        Task SendNotification(BuildDetails buildDetails);
    }
}
