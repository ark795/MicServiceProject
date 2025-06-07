using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Contracts.Common;

public record Fault<T>
{
    public string ErrorMessage { get; init; } = default!;
    public T OriginalMessage { get; init; } = default!;
    public DateTime OccurredAt { get; init; } = DateTime.UtcNow;
}
