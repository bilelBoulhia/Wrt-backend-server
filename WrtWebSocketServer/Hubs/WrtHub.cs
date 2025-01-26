using Microsoft.AspNetCore.SignalR;
using WrtWebSocketServer.Models;
using WrtWebSocketServer.Service;

namespace WrtWebSocketServer.Hubs
{
    public class WrtHub : Hub
    {
        private readonly ReportService _reportService;

        public WrtHub(ReportService reportService)
        {
            _reportService = reportService;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Connected", $"Connected {Context.ConnectionId}");
        }

        public async Task SendReport(Report report)
        {
            try
            {
          
                await _reportService.InsertReportAsync(report);

          
                await Clients.All.SendAsync("ReportReceived", report);
            }
            catch (ArgumentException ex)
            {
   
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
            catch (InvalidOperationException ex)
            {
    
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        public async Task GetReports(string trainRoute)
        {
            try
            {
 
                var reports = await _reportService.GetReportsByRouteAsync(trainRoute);

              
                await Clients.Caller.SendAsync("ReportsFetched", reports);
            }
            catch (Exception ex)
            {

                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }
    }
}
