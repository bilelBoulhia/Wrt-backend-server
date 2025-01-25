using Microsoft.AspNetCore.SignalR;

namespace WrtWebSocketServer.Hubs
{
    public class WrtHub : Hub
    {
        public async Task SendReports(string TrainRoute)
        {
           await Clients.All.SendAsync("SendingReports", TrainRoute);
        }
        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            await base.OnConnectedAsync();
        }
    }
}
