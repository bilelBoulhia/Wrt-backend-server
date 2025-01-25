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
            var key = $"TrainRoute:{report.TrainRoute}";
            var reportJson = JsonSerializer.Serialize(report);
            await _database.ListRightPushAsync(key, reportJson);
        }

        public async Task<List<Report>> GetReportsByRouteAsync(string trainRoute)
        {
            var key = $"TrainRoute:{trainRoute}";
            var reports = await _database.ListRangeAsync(key);
            return reports.Select(r => JsonSerializer.Deserialize<Report>(r!)).ToList()!;
        }
    }
}
                                                                                                                                            