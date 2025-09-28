using DomainLayer.Contracts;
using DomainLayer.Models.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    //msh hst5dm el storedbcontext ana msh ht3ml m3 el DB asln ana h3mlo  Redis [in-memory data structure store ]
    //m7taga anzl nuget package l Redis [StackExchange.Redis] 3lshan a ask el CLR eno y inject lia object mn class ykon b implement el  IConnectionMultiplexer
    //ana kda ft7t el connection m3 el Redis server
    public class BasketRepositroy(IConnectionMultiplexer connection) : IBasketRepository
    {
        //m7taga a3ml field l connection 3lshan a3rf awsl ll database bt3t el redis
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            // el fuction ely bt create bt5od mny RedisValue msh customerBasket object f h7wlo l Json
            var JsonBasket = JsonSerializer.Serialize(basket);
            var IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromDays(30));
            if (IsCreatedOrUpdated)
            {
                return await GetBasketAsync(basket.Id);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
          return await _database.KeyDeleteAsync(key);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string key)
        {
            var Basket=await _database.StringGetAsync(key);
            if(Basket.IsNullOrEmpty)
                return null;
            //el basket ragaly RedisValue w ana 3ayz a7wlo l CustomerBasket object
            return JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }
    }
}
