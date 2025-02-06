using Microsoft.AspNetCore.SignalR;
using NodaTime;
using WrtWebSocketServer.Handlers;
using WrtWebSocketServer.Models;
using WrtWebSocketServer.Service;

namespace WrtWebSocketServer.Hubs
{
    public class WrtHub : Hub
    {
        private readonly ReportService _reportService;
        private readonly SpamHandler _spamHandler;

        public WrtHub(ReportService reportService,SpamHandler spamHandler)
        {
            _reportService = reportService;
            _spamHandler = spamHandler;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Connected", $"Connected {Context.ConnectionId}");
        }

        public async Task SendReport(Report report)
        {
            try
            {
             

              

              
                report.ArrivalHour = SystemClock.Instance.GetCurrentInstant().InZone(DateTimeZoneProviders.Tzdb["Africa/Algiers"]).ToDateTimeUnspecified();  


                await _spamHandler.HandleOverReporting(report);
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
