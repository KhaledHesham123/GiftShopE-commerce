namespace UserProfileService.Shared.Entities
{
    public class ProductTag : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public string TagName { get; set; }
    }
}