using StackExchange.Redis;
using User.Services.Interfaces;

namespace User.Services.Service
{
    public class RedisService : IRedisService
    {
        private readonly ConnectionMultiplexer _redis;

        public RedisService(string connection)
        {
            _redis = ConnectionMultiplexer.Connect(connection);
        }
        public void SetValue(string key, string value, TimeSpan? expiry = null)
        {
            var db = _redis.GetDatabase();
            db.StringSet(key, value, expiry);
        }

        public string GetValue(string key)
        {
            var db = _redis.GetDatabase();
            return db.StringGet(key);
        }

        public void RemoveValue(string key)
        {
            var db = _redis.GetDatabase();
            db.KeyDelete(key);
        }
    }
}
