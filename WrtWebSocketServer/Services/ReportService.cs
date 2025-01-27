using StackExchange.Redis;
using System.Text.Json;
using WrtWebSocketServer.DatabaseContext;
using WrtWebSocketServer.Handlers;
using WrtWebSocketServer.Interfaces;
using WrtWebSocketServer.Models;

namespace WrtWebSocketServer.Service
{
    public class ReportService : IReport
    {
        private readonly RedisContext _database;
       

        public ReportService(RedisContext database)
        {
            _database = database;
 
        }

        public async Task InsertReportAsync( Report report) 
        {
            await _database.Insert<Report>(report,report.TrainRoute);
        }

        public async Task<List<Report>> GetReportsByRouteAsync(string trainRoute)
        {
            return await _database.GetList<Report>(trainRoute);
        }
    }
}
                                                                                                                                            