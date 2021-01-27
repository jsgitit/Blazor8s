using Blazor8s.Shared;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor8s.Server.Hubs
{
    public class GameHub : Hub<IGameHub>
    {
        private GameState _state;

        public GameHub(GameState state)
        {
            _state = state;
        }
        public async Task PlayerJoinGame(string name)
        {
            var player = new Player { Name = name };
            _state.Players.Add(player);
            await Groups.AddToGroupAsync(Context.ConnectionId, player.Id.ToString());
            await Clients.Group("table").PlayerJoined(name);
            await Clients.Caller.JoinedGame();

        }
        public async Task TableJoinGame()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"table");
            await Clients.Caller.JoinedGame();
            // var players = _state.Players;
        }

        public async Task StartGame()
        {
            _state.HasGameStarted = true;
            // Deal Players
            foreach (var player in _state.Players)
            {
                for (int i = 0; i < 5; i++)
                {
                    player.Hand.Add(_state.Deck.Pop());
                }
            }
            await Task.WhenAll(
                _state.Players
                    .Select(player => Clients.Group(player.Id.ToString()).AddHand(player.Hand))
                    );
            _state.HasGameStarted = true;
            await Clients.All.GameStarted();
        }


    }
}
