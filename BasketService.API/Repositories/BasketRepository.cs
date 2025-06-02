using BasketService.API.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace BasketService.API.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _database;

    public BasketRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<CustomerBasket?> GetBasket(string userId)
    {
        var data = await _database.StringGetAsync(userId);
        if (data.IsNullOrEmpty) return null;

        return JsonSerializer.Deserialize<CustomerBasket>(data!);
    }

    public async Task<CustomerBasket> UpdateBasket(CustomerBasket basket)
    {
        var serialized = JsonSerializer.Serialize(basket);
        await _database.StringSetAsync(basket.UserId, serialized);
        return basket;
    }

    public async Task<bool> DeleteBasket(string userId)
    {
        return await _database.KeyDeleteAsync(userId);
    }
}
