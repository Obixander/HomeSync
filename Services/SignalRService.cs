using Entities;
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
        public async Task SaveActivity(int FamilyId, Activity activity)
        {
            try
            {
                // Assuming 'SaveActivity' is a method on your SignalR hub that handles saving the activity
                await _hubConnection.InvokeAsync("SaveActivity", FamilyId.ToString(), activity);
            }
            catch (Exception ex)
            {
                // Handle any exceptions if the connection or invocation fails
                Console.WriteLine($"Error invoking SignalR method: {ex.Message}");
            }
        }
        public async Task UpdateActivity(int FamilyId, Activity activity)
        {
            try
            {
                await _hubConnection.InvokeAsync("UpdateActivity", FamilyId.ToString(), activity);
            }
            catch (Exception ex)
            {
                // Handle any exceptions if the connection or invocation fails
                Console.WriteLine($"Error invoking SignalR method: {ex.Message}");
            }
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
