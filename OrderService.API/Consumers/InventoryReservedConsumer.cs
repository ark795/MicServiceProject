using BuildingBlocks.Contracts.Events;
using MassTransit;

namespace OrderService.API.Consumers;

public class InventoryReservedConsumer : IConsumer<InventoryReservedEvent>
{
    private readonly ILogger<InventoryReservedConsumer> _logger;

    public InventoryReservedConsumer(ILogger<InventoryReservedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<InventoryReservedEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation("✅ [OrderService] Received InventoryReservedEvent for OrderId: {OrderId}", message.OrderId);

        // Simulate updating order status or notifying payment
        return Task.CompletedTask;
    }
}
