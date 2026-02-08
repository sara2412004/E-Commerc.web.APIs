using DomainLayer.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    //hst5dm el redis hena f hft7 connection 
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        //hena b3ml inject ll connection
        private readonly IDatabase _database =connection.GetDatabase();
        public async Task<string?> GetAsync(string CacheKey)
        {
            var CachValue =await _database.StringGetAsync(CacheKey);
            return CachValue.IsNullOrEmpty ? null: CachValue.ToString();
        }

        public async Task SetAsync(string CacheKey, string CacheValue, TimeSpan TimeToLive)
        {
           await _database.StringSetAsync(CacheKey, CacheValue, TimeToLive);

        }
    }
}
