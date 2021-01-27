using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using Blazor8s.Shared;
using Blazor8s.Client;
using System.Collections.Generic;

namespace Blazor8s.Client.Pages
{
    public partial class Game : IAsyncDisposable
    {
        [Inject] NavigationManager NavigationManager { get; set; }

        string userName;
        bool HasJoined = false;
        bool CanStartGame => HasJoined && !state.HasGameStarted; // if player has joined and game has not started, then Start Game button can render

        ClientGameState state = new();

        public bool IsConnected => hubConnection.State == HubConnectionState.Connected;

        private HubConnection hubConnection;

        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/gamehub"))
                .Build();

            hubConnection.On(nameof(IGameHub.JoinedGame), JoinedGame);
            hubConnection.On(nameof(IGameHub.GameStarted), GameStarted);
            hubConnection.On<List<Card>>(nameof(IGameHub.AddHand), AddHand);
            await hubConnection.StartAsync();
        }

        void JoinedGame()
        {
            HasJoined = true;
            StateHasChanged();
        }

        void AddHand(List<Card> hand)
        {
            state.Hand = hand;
        }

        void GameStarted()
        {
            state.HasGameStarted = true;
            StateHasChanged();

        }

        Task StartGame() => hubConnection.SendAsync("StartGame");

        Task JoinGame() => hubConnection.SendAsync("PlayerJoinGame", userName);

        public async ValueTask DisposeAsync()
        {
            await hubConnection.DisposeAsync();
        }

    }

}