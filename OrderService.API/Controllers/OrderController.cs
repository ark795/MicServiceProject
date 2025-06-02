using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.API.DTOs;
using OrderService.API.Models;
using OrderService.API.Repositories;
using System.Security.Claims;

namespace OrderService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _repo;
    private readonly IPublishEndpoint _publish;

    public OrderController(IOrderRepository repo, IPublishEndpoint publish)
    {
        _repo = repo;
        _publish = publish;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {
        order.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        order.CreatedAt = DateTime.UtcNow;
        var result = await _repo.CreateOrderAsync(order);

        await _publish.Publish(new OrderCreatedEvent
        {
            OrderId = result.Id,
            UserId = result.UserId,
            Total = result.Total,
            CreatedAt = result.CreatedAt
        });

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var orders = await _repo.GetOrdersByUserIdAsync(userId);
        return Ok(orders);
    }
}
