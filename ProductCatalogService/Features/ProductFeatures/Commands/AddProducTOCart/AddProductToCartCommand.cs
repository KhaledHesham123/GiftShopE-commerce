using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Shared.Entities;
using ProductCatalogService.Shared.Interfaces;
using ProductCatalogService.Shared.MasTranset.Events;

namespace ProductCatalogService.Features.ProductFeatures.Commands.AddProducTOCart
{
    public record AddProductToCartCommand(Guid userid, Guid ProductId,int Quantity) : IRequest<Result<bool>>;

    public class AddProductToCartCommandHandler : IRequestHandler<AddProductToCartCommand, Result<bool>>
    {
        private readonly IRepository<Product> repository;
        private readonly IPublishEndpoint publishService;

        public AddProductToCartCommandHandler(IRepository<Product> repository, IPublishEndpoint publishService)
        {
            this.repository = repository;
            this.publishService = publishService;
        }
        public async Task<Result<bool>> Handle(AddProductToCartCommand request, CancellationToken cancellationToken)
        {
            var product = await repository.GetQueryableByCriteria(x => x.Id == request.ProductId).FirstOrDefaultAsync();

            if (product == null)
            {
                return Result<bool>.FailResponse("this product is not exist", statusCode: 404);

            }

            await publishService.Publish(new ProductAddedToCartEvent
            {
                UserId = request.userid,
                Name = product.Name,
                Price = product.Price,
                ProductId = product.Id,
                Quantity=request.Quantity,
                //ProductImageUrl=product.ImageUrl

            });

            return Result<bool>.SuccessResponse(true);
        }
    }
}
