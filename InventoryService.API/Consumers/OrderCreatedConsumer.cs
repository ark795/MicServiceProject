using BuildingBlocks.Contracts.Events;
using MassTransit;

namespace InventoryService.API.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly ILogger<OrderCreatedConsumer> _logger;

    public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation("[InventoryService] Checking stock for Order {OrderId}", message.OrderId);

        // Simulate inventory reservation logic
        foreach (var item in message.Items)
        {
            _logger.LogInformation("Reserving {Quantity} of Product {ProductId}", item.Quantity, item.ProductId);
        }

        // Could publish InventoryReservedEvent next
        await Task.Delay(100); // simulate processing time

        // ✅ Publish InventoryReservedEvent
        await context.Publish(new InventoryReservedEvent
        {
            OrderId = message.OrderId,
            ReservedAt = DateTime.UtcNow
        });

        _logger.LogInformation("✅ Inventory reserved for OrderId: {OrderId}", message.OrderId);
    }
}
