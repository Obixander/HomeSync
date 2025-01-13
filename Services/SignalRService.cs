using Entities;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Diagnostics;

namespace Services
{
    public class SignalRService : IAsyncDisposable
    {
        private readonly HubConnection _hubConnection;
        
        public SignalRService()
        {
            _hubConnection = new HubConnectionBuilder().WithUrl(new Uri("https://localhost:7139/homesynchub")).WithAutomaticReconnect().Build(); 
        }

        /// <summary>
        /// This method is used to delete a list from the database
        /// </summary>
        /// <param name="FamilyId">The FamilyId belonging to a family</param>
        /// <param name="list">The list to be deleted</param>
        /// <returns>Void</returns>
        public async Task DeleteList(int FamilyId, CustomList list)
        {
            try
            {
                await _hubConnection.InvokeAsync("DeleteList", FamilyId.ToString(), list);
            }
            catch (Exception ex)
            {
                // Handle any exceptions if the connection or invocation fails
                Console.WriteLine($"Error invoking SignalR method: {ex.Message}");
            }
        }

        /// <summary>
        /// This method is used to update an list in the database
        /// </summary>
        /// <param name="FamilyId">The FamilyId belonging to a family</param>
        /// <param name="list">The list with the changes</param>
        /// <returns>void</returns>
        public async Task UpdateList(int FamilyId, CustomList list)
        {
            try
            {
                await _hubConnection.InvokeAsync("UpdateList", FamilyId, list);
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// This method is used to add an list to the database
        /// </summary>
        /// <param name="FamilyId">The FamilyId belonging to a family</param>
        /// <param name="list">The list to add to the database</param>
        /// <returns>void</returns>
        public async Task SaveList(int FamilyId, CustomList list)
        {
            try
            {
                await _hubConnection.InvokeAsync("SaveList", FamilyId.ToString(), list);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// This method is used to add an activity to the database
        /// </summary>
        /// <param name="FamilyId">The FamilyId belonging to a family</param>
        /// <param name="activity">The Activity to add to the database</param>
        /// <returns>void</returns>
        public async Task SaveActivity(int FamilyId, Entities.Activity activity)
        {
            try
            {
                await _hubConnection.InvokeAsync("SaveActivity", FamilyId.ToString(), activity);
            }
            catch (Exception ex)
            {
                // Handle any exceptions if the connection or invocation fails
                Console.WriteLine($"Error invoking SignalR method: {ex.Message}");
            }
        }

        /// <summary>
        /// This method is used to update an activity in the database
        /// </summary>
        /// <param name="FamilyId">The FamilyId belonging to a family</param>
        /// <param name="activity">The Activity with the changes</param>
        /// <returns>void</returns>
        public async Task UpdateActivity(int FamilyId, Entities.Activity activity)
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

        /// <summary>
        /// This method is used to delete an activity in the database
        /// </summary>
        /// <param name="FamilyId">The FamilyId belonging to a family</param>
        /// <param name="activity">The Activity to be deleted</param>
        /// <returns>void</returns>
        public async Task DeleteActivity(int FamilyId, Entities.Activity activity)
        {
            try
            {
                await _hubConnection.InvokeAsync("DeleteActivity", FamilyId.ToString(), activity);
            }
            catch (Exception ex)
            {
                // Handle any exceptions if the connection or invocation fails
                Console.WriteLine($"Error invoking SignalR method: {ex.Message}");
            }
        }

        /// <summary>
        /// This method is used to start the connection to the hub if the state is disconnected
        /// </summary>
        /// <returns>void</returns>
        public async Task StartConnectionAsync()
        {
            if (_hubConnection.State == HubConnectionState.Disconnected)
            {
                await _hubConnection.StartAsync();
            }

        }
        /// <summary>
        /// This method is used to stop the connection to the hub if the state is connected
        /// </summary>
        /// <returns></returns>
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
