using CatalogService.API.Models;
using MediatR;

namespace CatalogService.API.CQRS.Commands;

public class UpdateProductCommand : IRequest<Product?>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
