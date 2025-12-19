using CartService.Shared.basketRepository;
using CartService.Shared.Entites;
using CartService.Shared.Respones;
using MediatR;

namespace CartService.Feature.ShoppingCartFeature.Commands.addtProducToCart
{
    public record addProducToCartCommand(string ShoppingCartId,Guid userid ,Guid ProductId, string ProductName, decimal ProductPrice, string ProductImageUrl, int Quantity) : IRequest<RequestRespones<bool>>;

    public class addProducToCartCommandHandler : IRequestHandler<addProducToCartCommand, RequestRespones<bool>>
    {
        private readonly IbasketRepository basketRepo;

        public addProducToCartCommandHandler(IbasketRepository BasketRepo)
        {
            basketRepo = BasketRepo;
        }
        public async Task<RequestRespones<bool>> Handle(addProducToCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await basketRepo.GetCustomerBasket(request.ShoppingCartId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    Id = request.ShoppingCartId,
                    UserId = request.userid,
                    Items = new List<CartItem>() 
                };
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
                existingItem.Price = request.ProductPrice;
            }

            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId=request.ProductId,
                    ShoppingCartId = request.ShoppingCartId,
                    ProductName = request.ProductName,
                    Price = request.ProductPrice,
                    ProductImageUrl = request.ProductImageUrl,
                    Quantity = request.Quantity
                });
            }

            var updatedCart = await basketRepo.UpdateOrCustomerBasket(cart);

            if (updatedCart==null)
            {
                return RequestRespones<bool>.Fail("error while adding to Basket", 400);
            }

            return RequestRespones<bool>.Success(true);
        }
    }
}