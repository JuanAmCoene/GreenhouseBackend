using GreenhouseBackend.Models;
using Microsoft.AspNetCore.SignalR;

namespace GreenhouseBackend.Hubs
{
    public class ReadingsHub : Hub
    {
        public async Task SendReading(Reading reading)
        {
            await Clients.All.SendAsync("ReceiveReading", reading);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await Clients.Caller.SendAsync("Connected", $"Connected with ID: {Context.ConnectionId}");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
