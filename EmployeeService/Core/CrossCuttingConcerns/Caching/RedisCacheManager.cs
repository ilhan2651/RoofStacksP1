using Entities;
using Newtonsoft.Json;
using StackExchange.Redis;


namespace Core.CrossCuttingConcerns.Caching
{ 
    public class RedisCacheManager : ICacheManager
    {
        private readonly IDatabase _database;
        public RedisCacheManager(string connectionString)
        {

            var connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
            _database = connectionMultiplexer.GetDatabase();
        }
        public async Task Add(string key, object value)
        {
            await _database.StringSetAsync(key, JsonConvert.SerializeObject(value));
        }

        public async Task<Employee> Get(string key)
        {
            var value =await _database.StringGetAsync(key);
            if (value.IsNullOrEmpty)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<Employee>(value);
        }

        public async Task<List<Employee>> GetList(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.IsNullOrEmpty)
            {
                return new List<Employee>();
            }
            return JsonConvert.DeserializeObject<List<Employee>>(value);
        }

        public async  Task<bool> IsAdd(string key)
        {
            return await _database.KeyExistsAsync(key);
        }

        public async Task Remove(string key)
        {
             await _database.KeyDeleteAsync(key);
        }
    }
}
