using BuildingBlocks.Contracts.Events;
using MassTransit.Mediator;
using MediatR;

namespace OrderService.API.Application.Features.CreateOrder;

public record CreateOrderCommand(
    string UserId,
    decimal TotalPrice,
    AddressDto Address,
    List<OrderItemDto> Items
) : IRequest<Guid>;

