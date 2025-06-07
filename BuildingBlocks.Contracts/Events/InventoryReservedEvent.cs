using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Contracts.Events;

public record InventoryReservedEvent
{
    public Guid OrderId { get; init; }
    public DateTime ReservedAt { get; init; }
    //public bool IsReserved { get; init; }
    //public string Reason { get; init; } = string.Empty;
}
