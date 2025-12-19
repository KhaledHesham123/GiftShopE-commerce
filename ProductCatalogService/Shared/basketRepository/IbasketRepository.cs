using CartService.Shared;
using ProductCatalogService.Features.CartFeature.Queries.GetUserCart;

namespace ProductCatalogService.Shared.basketRepository
{
    public interface IbasketRepository
    {
        Task<ShoppingCartDto?>GetCustomerBasket(string basketId);

        Task<ShoppingCartDto?> UpdateOrCustomerBasket(ShoppingCartDto basket);

        Task<bool>DeleteCustomerBasket(string basketId);

    }
}
