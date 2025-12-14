using CartService.Shared.Entites;
using StackExchange.Redis;
using System.Text.Json;

namespace CartService.Shared.NewFolder
{
    public class BasketRepository : IbasketRepository
    {
        IDatabase _DbContext;
        public BasketRepository(IConnectionMultiplexer context)
        {
            _DbContext = context.GetDatabase();
        }

        public async Task<ShoppingCart?> GetCustomerBasket(string basketId)
        {
            var basket = await  _DbContext.StringGetAsync(basketId);
            var mappedBasket= basket.HasValue ?
                System.Text.Json.JsonSerializer.Deserialize<ShoppingCart>(basket) : null;
            return mappedBasket;
        }

        public async Task<ShoppingCart?> UpdateCustomerBasket(ShoppingCart basket)
        {
            var jsonbasket = JsonSerializer.Serialize(basket);

            var isCreated = await _DbContext.StringSetAsync(basket.Id, jsonbasket,TimeSpan.FromDays(30));
            if (isCreated)
            {
                return await GetCustomerBasket(basket.Id);
            }
            return null;
           

        }

        public async Task<bool> DeleteCustomerBasket(string basketId)
       => await _DbContext.KeyDeleteAsync(basketId);
    }
}
