using System.Collections.Generic;

namespace Ecommerce.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ThumbnailUrl { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }

        public List<ProductImage> Images { get; set; } = new List<ProductImage>();
        public List<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
    }
}
