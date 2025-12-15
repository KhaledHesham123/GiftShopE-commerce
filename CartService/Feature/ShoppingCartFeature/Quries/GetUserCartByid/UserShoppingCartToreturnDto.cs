using CartService.Shared.Entites;

namespace CartService.Feature.ShoppingCartFeature.Quries.GetUserCartByid
{
    public class UserShoppingCartToreturnDto
    {
        public string Id { get; set; }

        public Guid UserId { get; set; }

        public ICollection<CartItemDTo> Items { get; set; }
    }
}
