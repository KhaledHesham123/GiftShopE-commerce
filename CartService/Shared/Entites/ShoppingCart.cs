namespace CartService.Shared.Entites
{
    public class ShoppingCart
    {

        public string Id { get; set; }

        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdated { get; set; }
        public ICollection<CartItem> Items { get; set; }
    }
}
