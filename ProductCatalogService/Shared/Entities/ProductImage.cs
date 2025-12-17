namespace UserProfileService.Shared.Entities
{
    public class ProductImage:BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }
    }
}