using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Contracts.Events;

public record OrderCreatedEvent
{
    public Guid OrderId { get; init; }
    public string UserId { get; init; } = default!;
    public decimal TotalPrice { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public AddressDto ShippingAddress { get; init; } = default!;
    public List<OrderItemDto> Items { get; init; } = new();
}
