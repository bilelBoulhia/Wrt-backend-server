using StackExchange.Redis;
using System.Text.Json;
using WrtWebSocketServer.Interfaces;
using WrtWebSocketServer.Models;

namespace WrtWebSocketServer.Service
{
    public class ReportService : IReport

    {

        private readonly IDatabase _database;


        public ReportService(IConnectionMultiplexer connectionMultiplexer)
        {

            _database = connectionMultiplexer.GetDatabase();
        }
        public async Task InsertReportAsync( Report report) 
        {
            
            var reportJson = JsonSerializer.Serialize(report);
            await _database.ListRightPushAsync(report.TrainRoute, reportJson);
        }

        public async Task<List<Report>> GetReportsByRouteAsync(string trainRoute)
        {
        
            var reports = await _database.ListRangeAsync(trainRoute);
            return reports.Select(r => JsonSerializer.Deserialize<Report>(r!)).ToList()!;
        }
    }
}
                                                                                                                                            