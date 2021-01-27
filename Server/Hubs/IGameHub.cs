using System.Threading.Tasks;

namespace Blazor8s.Server.Hubs
{
    public interface IGameHub
    {
        Task JoinedGame();
        Task PlayerJoined(string player);
    }
}