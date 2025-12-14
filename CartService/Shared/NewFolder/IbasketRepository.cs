using CartService.Shared.Entites;

namespace CartService.Shared.NewFolder
{
    public interface IbasketRepository
    {
        Task<ShoppingCart?>GetCustomerBasket(string basketId);

        Task<ShoppingCart?> UpdateCustomerBasket(ShoppingCart basket);

        Task<bool>DeleteCustomerBasket(string basketId);

    }
}
