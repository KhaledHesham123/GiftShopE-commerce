using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Shared.Entities;
using ProductCatalogService.Shared.Interfaces;
using StackExchange.Redis;

namespace ProductCatalogService.Features.ProductFeatures.Queries.GetProductsByids
{
    public record GetProductsByidsQuery(IEnumerable<Guid> ProductsIds):IRequest<Result<IEnumerable<ProductTOReturnDto>>>;

    public class GetProductsByidsQueryHandler : IRequestHandler<GetProductsByidsQuery, Result<IEnumerable<ProductTOReturnDto>>>
    {
        private readonly IRepository<Product> repository;

        public GetProductsByidsQueryHandler(IRepository<Product> repository)
        {
            this.repository = repository;
        }
        public async Task<Result<IEnumerable<ProductTOReturnDto>>> Handle(GetProductsByidsQuery request, CancellationToken cancellationToken)
        {
            var products = await repository.GetAll().Where(x=> request.ProductsIds.Contains(x.Id))
                .Select(
                x=>new ProductTOReturnDto 
                {
                    Name=x.Name,
                    Price=x.Price,
                }
                ).ToListAsync();

            if (!products.Any())
            {
                return Result<IEnumerable<ProductTOReturnDto>>.FailResponse("No products found with the provided IDs.");

            }

            return Result<IEnumerable<ProductTOReturnDto>>.SuccessResponse(products);
        }
    }


}
