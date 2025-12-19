using MassTransit.Initializers;
using MediatR;
using ProductCatalogService.Features.Shared;
using ProductCatalogService.Shared.basketRepository;
using System.Net.Http;
using System.Text.Json;

namespace ProductCatalogService.Features.CartFeature.Queries.GetUserCart
{
    public record GetUserCartCommand(Guid UserId): IRequest<Result<ShoppingCartDto>>;

    public class GetUserCartCommandHandler : IRequestHandler<GetUserCartCommand, Result<ShoppingCartDto>>
    {
        private readonly IMediator mediator;
        private readonly IbasketRepository basketrepository;

        public GetUserCartCommandHandler(IMediator mediator, IbasketRepository Basketrepository )
        {
            this.mediator = mediator;
            basketrepository = Basketrepository;
        }
        public async Task<Result<ShoppingCartDto>> Handle(GetUserCartCommand request, CancellationToken cancellationToken)
        {
            var userCart = await GetUserCart(request.UserId.ToString());

            if (userCart == null)
            {
                return Result<ShoppingCartDto>.FailResponse("there is no cart for this user");
            }

            return Result<ShoppingCartDto>.SuccessResponse(userCart);

        }

        #region MyRegion
        public async Task<ShoppingCartDto?> GetUserCart(string Basketid)
        {
            var basket = await basketrepository.GetCustomerBasket(Basketid);

            if (basket == null) return null;

            return new ShoppingCartDto
            {
                Id = basket.Id,
                UserId = basket.UserId,
                Items = basket.Items.Select(it => new CartItemDto
                {
                    ShoppingCartId = it.ShoppingCartId,
                    Price = it.Price,
                    ProductImageUrl = it.ProductImageUrl,
                    ProductName = it.ProductName,
                    Quantity = it.Quantity
                }).ToList()
            };

        } 
        #endregion
    }
}
