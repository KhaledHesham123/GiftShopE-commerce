using ProductCatalogService.Shared.Entities;
using ProductCatalogService.Shared.Interfaces;
using MediatR;

namespace ProductCatalogService.Features.Shared.Queries.CheckExist
{
    public class CheckExistQueryHandler<T> : IRequestHandler<CheckExistQuery<T>, Result<bool>> where T : BaseEntity
    {
        private readonly IRepository<T> _repository;

        public CheckExistQueryHandler(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<Result<bool>> Handle(CheckExistQuery<T> request, CancellationToken cancellationToken)
        {
            var existsEntity = await _repository.ExistsAsync(request.Predicate, cancellationToken);
            return Result<bool>.Response(existsEntity);
        }
    }
}
