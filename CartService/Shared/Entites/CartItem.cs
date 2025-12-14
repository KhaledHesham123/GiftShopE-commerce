namespace CartService.Shared.Entites
{
    public class CartItem
    {
        public int Id { get; set; }
        public string ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}