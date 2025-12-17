using UserProfileService.Shared.Entities;
using MediatR;
using System.Linq.Expressions;

namespace UserProfileService.Features.Shared.Queries.GetByCriteria
{
    public class GetByCriteriaQuery<TRequest,TResponse> : IRequest<Result<TResponse>> where TRequest :  BaseEntity
    {
        public Expression<Func<TRequest, bool>> Criteria { get; set; }
        public Expression<Func<TRequest, TResponse>> Selector { get; set; }

    }
}
