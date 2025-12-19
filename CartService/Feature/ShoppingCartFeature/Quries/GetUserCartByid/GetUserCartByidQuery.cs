using CartService.Shared.basketRepository;
using CartService.Shared.Respones;
using MassTransit.Initializers;
using MediatR;

namespace CartService.Feature.ShoppingCartFeature.Quries.GetUserCartByid
{
    public record GetUserCartByidQuery(string Basketid):IRequest<RequestRespones<UserShoppingCartToreturnDto>>;

    public class GetUserCartByidQueryHandler : IRequestHandler<GetUserCartByidQuery, RequestRespones<UserShoppingCartToreturnDto>>
    {
        private readonly IbasketRepository basketRepository;

        public GetUserCartByidQueryHandler(IbasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }
        public async Task<RequestRespones<UserShoppingCartToreturnDto>> Handle(GetUserCartByidQuery request, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetCustomerBasket(request.Basketid).
                Select(x => new UserShoppingCartToreturnDto 
                {
                    Id=x.Id,
                    Items=x.Items.Select(it=>new CartItemDTo 
                    {
                        ShoppingCartId=it.ShoppingCartId,
                        Price=it.Price,
                        ProductImageUrl=it.ProductImageUrl,
                        ProductName=it.ProductName,
                        Quantity=it.Quantity
                    }).ToList(),
                    UserId=x.UserId
                });

            if (basket==null)
            {
                return RequestRespones<UserShoppingCartToreturnDto>.Fail("there is no Cart for this id");
            }

            return RequestRespones<UserShoppingCartToreturnDto>.Success(basket);    
        }
    }
}
