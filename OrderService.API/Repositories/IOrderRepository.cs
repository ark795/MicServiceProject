using OrderService.API.Models;

namespace OrderService.API.Repositories;

public interface IOrderRepository
{
    Task<List<Order>> GetOrdersByUserIdAsync(string userId);
    Task<Order> CreateOrderAsync(Order order);
}
