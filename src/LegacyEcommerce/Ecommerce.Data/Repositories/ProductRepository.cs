using System.Collections.Generic;
using System.Linq;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.ViewModels;
using CoreModels = Ecommerce.Core.Models;

namespace Ecommerce.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public IEnumerable<CoreModels.Product> Query(ProductFilterViewModel filter)
        {
            using (var db = new LegacyEcommerceDbEntities())
            {
                var q = db.Products.Where(p => p.IsActive);

                if (filter != null)
                {
                    if (filter.CategoryId.HasValue)
                        q = q.Where(p => p.CategoryId == filter.CategoryId.Value);
                    if (filter.MinPrice.HasValue)
                        q = q.Where(p => p.Price >= filter.MinPrice.Value);
                    if (filter.MaxPrice.HasValue)
                        q = q.Where(p => p.Price <= filter.MaxPrice.Value);

                    switch (filter.SortBy)
                    {
                        case "price_asc":  q = q.OrderBy(p => p.Price); break;
                        case "price_desc": q = q.OrderByDescending(p => p.Price); break;
                        default:           q = q.OrderBy(p => p.Name); break;
                    }
                }
                else
                {
                    q = q.OrderBy(p => p.Name);
                }

                return q.ToList().Select(MapToCore).ToList();
            }
        }

        public CoreModels.Product GetById(int id)
        {
            using (var db = new LegacyEcommerceDbEntities())
            {
                var p = db.Products.FirstOrDefault(x => x.Id == id);
                if (p == null) return null;

                var product = MapToCore(p);
                product.Images = db.ProductImages
                    .Where(i => i.ProductId == id)
                    .OrderBy(i => i.SortOrder)
                    .ToList()
                    .Select(i => new CoreModels.ProductImage
                    {
                        Id = i.Id, ProductId = i.ProductId, Url = i.Url, SortOrder = i.SortOrder
                    }).ToList();
                product.Variants = db.ProductVariants
                    .Where(v => v.ProductId == id)
                    .ToList()
                    .Select(v => new CoreModels.ProductVariant
                    {
                        Id = v.Id, ProductId = v.ProductId, Name = v.Name
                    }).ToList();
                return product;
            }
        }

        private static CoreModels.Product MapToCore(Product p)
        {
            return new CoreModels.Product
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ThumbnailUrl = p.ThumbnailUrl,
                CategoryId = p.CategoryId,
                IsActive = p.IsActive
            };
        }
    }
}
