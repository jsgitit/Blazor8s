using Blazor8s.Shared;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor8s.Server.Hubs
{
    public class GameHub : Hub<IGameHub>
    {
        private readonly GameState _state;
        
        public GameHub(GameState state) => _state = state;
        
        public async Task PlayerJoinGame(string name)
        {
            var player = new Player { Name = name };
            _state.Players[player.Id] = player;
            await Groups.AddToGroupAsync(Context.ConnectionId, player.Id.ToString());
            await Clients.Group("table").PlayerJoined(name);
            await Clients.Caller.JoinedGame(player.Id);

        }
        public async Task TableJoinGame()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "table");
            await Clients.Caller.JoinedGame(Guid.NewGuid());
        }

        public async Task StartGame()
        {
            // Deal Players

            // TODO: consider Enumberable.Range() operator instead of nested loops?
            // foreach (var (id, player) in _state.Players)  // this statement destructures id and player from dictionary
            for (int i = 0; i < 5; i++)
            {
                foreach (var (_, player) in _state.Players) // revised the vars section to throw away the id with underscore
                {
                    player.Hand.Add(_state.Deck.Pop());
                }
            }

            await Task.WhenAll(
                _state.Players
                    .Select(kvp => Clients.Group(kvp.Key.ToString())
                    .AddHand(kvp.Value.Hand)));

            _state.HasGameStarted = true;
            await Clients.All.GameStarted();
            _state.LastDiscard = _state.Deck.Pop();
            await Clients.Group("table").GameStarted(_state.Deck.Count, _state.LastDiscard);
        }

        public async Task DrawCard(Guid id)
        {
            var player = _state.Players[id];
            var newCard = _state.Deck.Pop();
            player.Hand.Add(newCard);

            await Clients.Group(id.ToString()).AddCardToHand(newCard);
            await Clients.Group("table").UpdateDeckCount(_state.Deck.Count);
        }

        public async Task PlayCard(Guid id, Card card)
        {
            // remove the card from the player's hand
            var player = _state.Players[id];
            player.Hand.Remove(card);

            // add to discard pile
            _state.LastDiscard = card;

            // notify the table
            await Clients.Group("table").DiscardPlayed(card);
            // notify player if it was successfull or not
            await Clients.Caller.DiscardPlayed(card);

        }
    }
}
