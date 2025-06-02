using MediatR;

namespace CatalogService.API.CQRS.Commands;

public class DeleteProductCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
