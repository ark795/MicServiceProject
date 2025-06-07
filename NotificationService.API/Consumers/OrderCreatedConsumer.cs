using BuildingBlocks.Contracts.Events;
using MassTransit;

namespace NotificationService.API.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly ILogger<OrderCreatedConsumer> _logger;

    public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("[NotificationService] Received OrderCreatedEvent for OrderId: {OrderId}, Total: {TotalPrice}",
            message.OrderId, message.TotalPrice);

        return Task.CompletedTask;
    }
}
