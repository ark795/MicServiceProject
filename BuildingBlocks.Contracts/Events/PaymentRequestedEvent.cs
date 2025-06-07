using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Contracts.Events;

public record PaymentRequestedEvent
{
    public Guid OrderId { get; init; }
    public string UserId { get; init; } = default!;
    public decimal Amount { get; init; }
    public string PaymentMethod { get; init; } = default!;
    public DateTime RequestedAt { get; init; } = DateTime.UtcNow;
}
