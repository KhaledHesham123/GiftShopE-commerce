using ProductCatalogService.Shared.Entities;
using MediatR;
using System.Linq.Expressions;

namespace ProductCatalogService.Features.Shared.Queries.CheckExist
{
    public record CheckExistQuery<T>(Expression<Func<T, bool>> Predicate) : IRequest<Result<bool>> where T : BaseEntity;
}
