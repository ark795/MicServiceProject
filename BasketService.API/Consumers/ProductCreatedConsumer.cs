using BasketService.API.Repositories;
using CatalogService.API.Events;
using MassTransit;

namespace BasketService.API.Consumers;

public class ProductCreatedConsumer : IConsumer<ProductCreatedEvent>
{
    private readonly IBasketRepository _repository;

    public ProductCreatedConsumer(IBasketRepository repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
    {
        var message = context.Message;

        Console.WriteLine($"[BasketService] Received ProductCreatedEvent: {message.Name} - {message.Price}");

        // Optional: Sync data or log it
    }
}
