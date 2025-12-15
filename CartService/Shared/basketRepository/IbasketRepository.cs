using CartService.Shared.Entites;

namespace CartService.Shared.basketRepository
{
    public interface IbasketRepository
    {
        Task<ShoppingCart?>GetCustomerBasket(string basketId);

        Task<ShoppingCart?> UpdateOrCustomerBasket(ShoppingCart basket);

        Task<bool>DeleteCustomerBasket(string basketId);

    }
}
