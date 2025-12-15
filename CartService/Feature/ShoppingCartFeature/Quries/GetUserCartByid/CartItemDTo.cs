namespace CartService.Feature.ShoppingCartFeature.Quries.GetUserCartByid
{
    public class CartItemDTo
    {

        public string ShoppingCartId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ProductImageUrl { get; set; }

        public int Quantity { get; set; }
    }
}