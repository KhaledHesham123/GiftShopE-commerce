using CartService.Shared;
using ProductCatalogService.Features.CartFeature.Queries.GetUserCart;
using ProductCatalogService.Shared.basketRepository;
using StackExchange.Redis;
using System.Text.Json;

namespace CartService.Shared.basketRepository
{
    public class BasketRepository : IbasketRepository
    {
        IDatabase _DbContext;
        public BasketRepository(IConnectionMultiplexer context)
        {
            _DbContext = context.GetDatabase();
        }

        public async Task<ShoppingCartDto?> GetCustomerBasket(string basketId)
        {
            var basket = await  _DbContext.StringGetAsync(basketId);
            var mappedBasket= basket.HasValue ?
                System.Text.Json.JsonSerializer.Deserialize<ShoppingCartDto>(basket) : null;
            return mappedBasket;
        }

        public async Task<ShoppingCartDto?> UpdateOrCustomerBasket(ShoppingCartDto basket)
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
