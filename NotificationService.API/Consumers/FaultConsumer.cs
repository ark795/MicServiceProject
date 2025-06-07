using BuildingBlocks.Contracts.Events;
using MassTransit;

namespace NotificationService.API.Consumers;

public class FaultConsumer : IConsumer<Fault<OrderCreatedEvent>>
{
    private readonly ILogger<FaultConsumer> _logger;

    public FaultConsumer(ILogger<FaultConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<Fault<OrderCreatedEvent>> context)
    {
        _logger.LogError("💥 Fault in OrderCreatedEvent: {Exception}", context.Message.Exceptions.First().Message);
        return Task.CompletedTask;
    }
}
