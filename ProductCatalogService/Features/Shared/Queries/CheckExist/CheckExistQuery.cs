using UserProfileService.Shared.Entities;
using MediatR;
using System.Linq.Expressions;

namespace UserProfileService.Features.Shared.Queries.CheckExist
{
    public record CheckExistQuery<T>(Expression<Func<T, bool>> Predicate) : IRequest<Result<bool>> where T : BaseEntity;
}
