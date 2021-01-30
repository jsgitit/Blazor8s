using Blazor8s.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor8s.Client.Pages
{
    public partial class Table : IAsyncDisposable
    {

        private HubConnection _hubConnection;
        private bool _hasJoinedGame = false;
        private int _deckCount = 52;
        private Card _discardCard;
        private List<string> _players = new();

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool IsConnected => _hubConnection.State == HubConnectionState.Connected;
        protected override async Task OnInitializedAsync()
        {
            _hubConnection = new HubConnectionBuilder()
              .WithUrl(NavigationManager.ToAbsoluteUri("/gamehub"))
              .Build();

            _hubConnection.On("JoinedGame", () =>
            {
                _hasJoinedGame = true;
                StateHasChanged();  // updates UI
            });

            _hubConnection.On<string>(nameof(IGameHub.PlayerJoined), PlayerJoined);
            _hubConnection.On<int, Card>(nameof(IGameHub.GameStarted), GameStarted);
            _hubConnection.On<Card>(nameof(IGameHub.DiscardPlayed), DiscardPlayed);
            _hubConnection.On<int>(nameof(IGameHub.UpdateDeckCount), UpdateDeckCount);

            await _hubConnection.StartAsync();

            // Excute a method to a message back to GameHub
            await _hubConnection.SendAsync("TableJoinGame");  // TableJoinGame is a Method in GameHub

        }

        void GameStarted(int Count, Card card)
        {
            _deckCount = Count;
            _discardCard = card;
            StateHasChanged();

        }
        void UpdateDeckCount(int count)
        {
            _deckCount = count;
            StateHasChanged();

        }
        void DiscardPlayed(Card card)
        {
            _discardCard = card;
            StateHasChanged();

        }
        void PlayerJoined(string player)
        {
            _players.Add(player);
            StateHasChanged();
        }

        //public async ValueTask DisposeAsync()
        //{
        //    await hubConnection.DisposeAsync();
        //}
        ValueTask IAsyncDisposable.DisposeAsync() => _hubConnection.DisposeAsync();
    }
}
