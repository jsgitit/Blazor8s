using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor8s.Shared
{
    public interface IGameHub
    {
        Task JoinedGame();
        Task PlayerJoined(string player);
        Task GameStarted();
        Task AddHand(List<Card> hand);
    }
}