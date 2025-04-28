﻿using Domain.Contract;
using Domain.Entities;
using StackExchange.Redis;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Presistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly StackExchange.Redis.IDatabase _database = connection.GetDatabase();
        public async Task<bool> DeleteBasketAsync(string id)
             => await _database.KeyDeleteAsync(id); 
        

        public async Task<CustomerBasket> GetBasketAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);

            if (basket.IsNullOrEmpty)
                return null;

            return JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            var serializedBasket = JsonSerializer.Serialize(basket);

            var isCreatedOrUpdated = await _database.StringSetAsync( basket.Id.ToString()  , serializedBasket, timeToLive ?? TimeSpan.FromDays(30));

            return isCreatedOrUpdated ? await GetBasketAsync(basket.Id.ToString()) : null;



        }
    }
}
