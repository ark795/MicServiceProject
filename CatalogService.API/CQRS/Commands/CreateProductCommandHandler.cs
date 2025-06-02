using CatalogService.API.Data;
using CatalogService.API.Events;
using CatalogService.API.Models;
using MassTransit;
using MediatR;

namespace CatalogService.API.CQRS.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateProductCommandHandler(ApplicationDbContext context, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        //publish event
        await _publishEndpoint.Publish(new ProductCreatedEvent
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        });

        return product;
    }
}
