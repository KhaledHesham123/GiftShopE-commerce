using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalogService.Shared.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal TaxRate { get; set; } = 0;

        public decimal? DiscountPrice { get; set; }

        public int StockQuantity { get; set; }
        public int MinStockLevel { get; set; }
        public int MaxStockLevel { get; set; }

        public string MainImageUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsBestSeller { get; set; }

        public ICollection<ProductAttribute> Attributes { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductTag> Tags { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<ProductOccasion> ProductOccasions { get; set; }
    }

}
