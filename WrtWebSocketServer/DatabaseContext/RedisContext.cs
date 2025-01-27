
using StackExchange.Redis;

using System.Text.Json;
using WrtWebSocketServer.Models;

namespace WrtWebSocketServer.DatabaseContext
{
    public class RedisContext
    {
        private readonly IDatabase _database;
        private const int ExpirationTimeInSeconds = 24 * 60 * 60;
        private static RedisContext instance = null;
        private static object padlock = new object();

        public RedisContext(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }
       
        public async Task Insert<T>(T obj,string key)
        {
            var objJson = JsonSerializer.Serialize<T>(obj);
            await _database.ListRightPushAsync(key, objJson);
            await _database.KeyExpireAsync(key, TimeSpan.FromSeconds(ExpirationTimeInSeconds));

        }
        public async Task<List<T>> GetList<T>(string key)
        {
            return (await _database.ListRangeAsync(key))
                .Select(r => JsonSerializer.Deserialize<T>(r!))
                .ToList()!;
        }

       

    }
}
