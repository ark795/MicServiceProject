using CatalogService.API.Models;
using MediatR;

namespace CatalogService.API.CQRS.Commands;

public class CreateProductCommand : IRequest<Product>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
