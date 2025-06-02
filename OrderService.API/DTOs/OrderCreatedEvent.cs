namespace OrderService.API.DTOs;

public class OrderCreatedEvent
{
    public string OrderId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
}
