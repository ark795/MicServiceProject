using CatalogService.API.Models;
using MediatR;

namespace CatalogService.API.CQRS.Queries;

public class GetAllProductsQuery : IRequest<List<Product>>
{
}
