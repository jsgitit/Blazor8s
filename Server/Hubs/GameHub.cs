using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor8s.Server.Hubs
{
    public class GameHub : Hub
    {
        private GameState _state;

        public GameHub(GameState state)
        {
            _state = state;
        }
        public async Task PlayerJoinGame(string user)
        {
            _state.Players.Add(user);
            await Clients.Group("table").SendAsync("playerJoined", user);
            
        }
        public async Task TableJoinGame(string user)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"table");
            await Clients.Caller.SendAsync("Joined Table");
            // var players = _state.Players;
        }
    }
}
