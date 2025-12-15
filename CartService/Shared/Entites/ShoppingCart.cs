namespace CartService.Shared.Entites
{
    public class ShoppingCart
    {

        public string Id { get; set; }

        public Guid UserId { get; set; }
      
        public ICollection<CartItem> Items { get; set; }
    }
}
