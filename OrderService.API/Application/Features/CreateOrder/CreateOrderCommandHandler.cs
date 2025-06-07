using BuildingBlocks.Contracts.Events;
using MassTransit;
using MediatR;

namespace OrderService.API.Application.Features.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateOrderCommandHandler(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // 1. Save to database (assumed done here, simplified)
        var orderId = Guid.NewGuid(); // in reality, you'd save and get this from DB

        // 2. Publish the event
        var orderCreatedEvent = new OrderCreatedEvent
        {
            OrderId = orderId,
            UserId = request.UserId,
            TotalPrice = request.TotalPrice,
            CreatedAt = DateTime.UtcNow,
            ShippingAddress = request.Address,
            Items = request.Items
        };

        await _publishEndpoint.Publish(orderCreatedEvent, cancellationToken);

        return orderId;
    }
}
