using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        //hover over idatabase, sve jasno, redis baza
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            //now we got connection with redis database
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            //hover over, returns true if the key was deleted
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            //our baskets are going to be stored as strings in our redis database
            var data = await _database.StringGetAsync(basketId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            //when updating, we will simply replace existing string with the new one
            //timespan is matter of business decision, we will keep the basket for 30 days
            //trebaš uvijek voditi računa tu koliko memorije imaš
            var created = await _database.StringSetAsync(basket.Id, 
                JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

            if (!created) return null;

            //ovdje deserijaliziraš
            return await GetBasketAsync(basket.Id);
        }
    }
}