using StackExchange.Redis;
using WrtWebSocketServer.DatabaseContext;
using WrtWebSocketServer.Models;

namespace WrtWebSocketServer.Handlers
{
    public class SpamHandler
    {

        private readonly RedisContext _database;


        public SpamHandler(RedisContext database)
        {
            _database = database;
        }

        public async Task HandleOverReporting(Report report)
        {
            var list = await _database.GetList<Report>(report.TrainRoute);
            foreach (Report rp in list)
            {

                if (report.CurrentGare != rp.CurrentGare)
                    continue;

                if (report.ArrivalHour - rp.ArrivalHour  < TimeSpan.FromMinutes(5))
                    {
                        throw new InvalidOperationException("report already exist");
                    }
                
            }
        }
    }
}
