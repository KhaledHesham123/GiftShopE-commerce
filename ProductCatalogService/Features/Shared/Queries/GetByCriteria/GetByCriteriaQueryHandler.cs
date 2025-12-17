using UserProfileService.Data.DBContexts;
using UserProfileService.Shared.Entities;
using UserProfileService.Shared.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace UserProfileService.Features.Shared.Queries.GetByCriteria
{
    public class GetByCriteriaQueryHandler<TRequest, TReaponse> : IRequestHandler<GetByCriteriaQuery<TRequest, TReaponse>, Result<TReaponse>> where TRequest : BaseEntity
    {
        //private readonly IRepository<TRequest> _repository;
        private ProductCatalogDbContext _dbContext;

        public GetByCriteriaQueryHandler(ProductCatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<TReaponse>> Handle(GetByCriteriaQuery<TRequest, TReaponse> request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Set<TRequest>().Where(request.Criteria).Select(request.Selector).FirstOrDefaultAsync(cancellationToken);
            return Result<TReaponse>.SuccessResponse(result, "Data fetched successfully", 200);
        }
    }
}
