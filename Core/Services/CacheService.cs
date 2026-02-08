using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service
{
    public class CacheService(ICacheRepository repository) : ICacheService
    {
        public async Task<string?> GetAsync(string CacheKey)
        {
            return await repository.GetAsync(CacheKey);
        }

        public async Task SetAsync(string CacheKey, object CacheValue, TimeSpan TimeToLive)
        {
            var CacheValueString = JsonSerializer.Serialize(CacheValue);
            await repository.SetAsync(CacheKey, CacheValueString, TimeToLive);  
        }
    }
}
