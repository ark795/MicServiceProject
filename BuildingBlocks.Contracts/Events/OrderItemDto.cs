namespace BuildingBlocks.Contracts.Events;

public record OrderItemDto
{
    public string ProductId { get; init; } = default!;
    public string ProductName { get; init; } = default!;
    public int Quantity { get; init; }
    public decimal Price { get; init; }
}