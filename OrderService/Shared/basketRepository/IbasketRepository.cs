
using OrderService.Shared.Entites;

namespace ProductCatalogService.Shared.basketRepository
{
    public interface IbasketRepository
    {
        Task<ShoppingCart?>GetCustomerBasket(string basketId);

        Task<ShoppingCart?> UpdateOrCustomerBasket(ShoppingCart basket);

        Task<bool>DeleteCustomerBasket(string basketId);

    }
}
