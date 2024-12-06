using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace Services
{
    public class SignalRService : IAsyncDisposable
    {
        private readonly HubConnection _hubConnection;
        
        public SignalRService()
        {
            _hubConnection = new HubConnectionBuilder().WithUrl(new Uri("https://localhost:7139/homesynchub")).WithAutomaticReconnect().Build(); 
        }

        public async Task StartConnectionAsync()
        {
            if (_hubConnection.State == HubConnectionState.Disconnected)
            {
                await _hubConnection.StartAsync();
            }

        }

        public async Task StopConnectionAsync()
        {
            if (_hubConnection.State == HubConnectionState.Connected)
            {
                await _hubConnection.StopAsync();
            }
        }

        private async Task ReconnectAsync()
        {
            if (_hubConnection.State == HubConnectionState.Disconnected)
            {
                try
                {
                    // Wait for a few seconds before retrying connection (implement your retry strategy here)
                    await _hubConnection.StartAsync();
                }
                catch (Exception ex)
                {
                    // Handle the exception (e.g., logging)
                    Console.WriteLine($"Error reconnecting: {ex.Message}");
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection != null)
            {
                await _hubConnection.DisposeAsync();
            }
        }

        public HubConnection HubConnection => _hubConnection;
    }
}
