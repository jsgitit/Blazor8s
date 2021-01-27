using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor8s.Shared;

namespace Blazor8s.Server.Hubs
{
    public class GameHub : Hub<IGameHub> 
    {
        private GameState _state;

        public GameHub(GameState state)
        {
            _state = state;
        }
        public async Task PlayerJoinGame(string player)
        {
            _state.Players.Add(new Player { Name = player});
            await Clients.Group("table").PlayerJoined(player);
            await Clients.Caller.JoinedGame();

        }
        public async Task TableJoinGame()  // i still had an extra username param on this method, so calling it never worked until I removed the param!
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"table");
            await Clients.Caller.JoinedGame();
            // var players = _state.Players;
        }
    }
}
