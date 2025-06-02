using CatalogService.API.Data;
using CatalogService.API.Events;
using CatalogService.API.Models;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.API.CQRS.Commands;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product?>
{
    private readonly ApplicationDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;

    public UpdateProductCommandHandler(ApplicationDbContext context, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Product?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);

        if (product == null) return null;

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;

        await _context.SaveChangesAsync();

        await _publishEndpoint.Publish(new ProductCreatedEvent
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
        });
        return product;
    }
}