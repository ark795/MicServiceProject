using MongoDB.Driver;
using OrderService.API.Models;

namespace OrderService.API.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IMongoCollection<Order> _collection;

    public OrderRepository(IConfiguration config)
    {
        var client = new MongoClient(config["MongoSettings:ConnectionString"]);
        var database = client.GetDatabase(config["MongoSettings:Database"]);
        _collection = database.GetCollection<Order>("Orders");
    }

    public async Task<List<Order>> GetOrdersByUserIdAsync(string userId) =>
        await _collection.Find(o => o.UserId == userId).ToListAsync();

    public async Task<Order> CreateOrderAsync(Order order)
    {
        await _collection.InsertOneAsync(order);
        return order;
    }
}
{
}
