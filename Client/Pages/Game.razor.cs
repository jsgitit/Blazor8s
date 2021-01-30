using Blazor8s.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor8s.Client.Pages
{
    public partial class Game : IAsyncDisposable
    {
        [Inject] NavigationManager NavigationManager { get; set; }

        private string _userName;
        private bool _hasJoined = false;
        private bool _canStartGame => _hasJoined && !_state.HasGameStarted; // if player has joined and game has not started, then Start Game button can render
        private ClientGameState _state = new();
        private HubConnection _hubConnection;
        public bool IsConnected => _hubConnection.State == HubConnectionState.Connected;

        protected override async Task OnInitializedAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/gamehub"))
                .Build();

            _hubConnection.On<Guid>(nameof(IGameHub.JoinedGame), JoinedGame);
            _hubConnection.On(nameof(IGameHub.GameStarted), GameStarted);
            _hubConnection.On<List<Card>>(nameof(IGameHub.AddHand), AddHand);
            _hubConnection.On<Card>(nameof(IGameHub.AddCardToHand), AddCardToHand);
            _hubConnection.On<Card>(nameof(IGameHub.DiscardPlayed), DiscardPlayed);
            await _hubConnection.StartAsync();
        }

        void DiscardPlayed(Card card)
        {
            _state.SelectedCard = null;
            var playerCard = _state.Hand.Find(c => c.Suit == card.Suit && c.Value == card.Value);
            _state.Hand.Remove(playerCard);
            StateHasChanged();

        }

        private void HandleSelectedCard(Card card) => 
            _state.SelectedCard = _state.SelectedCard == card ? null : card;

        private void JoinedGame(Guid id)
        {
            _state.Id = id;
            _hasJoined = true;
            StateHasChanged();
        }

        private void AddHand(List<Card> hand) => _state.Hand = hand;

        private void AddCardToHand(Card card)
        {
            _state.Hand.Add(card);
            StateHasChanged();
        }
        private Task PlayCard() => _hubConnection.SendAsync("PlayCard", _state.Id, _state.SelectedCard);

        private void GameStarted()
        {
            _state.HasGameStarted = true;
            StateHasChanged();

        }

        private Task StartGame() => _hubConnection.SendAsync("StartGame");
        private Task DrawCard() => _hubConnection.SendAsync("DrawCard", _state.Id);
        private Task JoinGame() => _hubConnection.SendAsync("PlayerJoinGame", _userName);

        public async ValueTask DisposeAsync()
        {
            await _hubConnection.DisposeAsync();
        }

    }

}