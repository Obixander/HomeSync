using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Services;
using Website.Client.Data;

namespace Website.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            //builder.Services.AddSingleton(new SignalRService());
            //builder.Services.AddSingleton(new SessionToken());

            await builder.Build().RunAsync();
        }
    }
}
