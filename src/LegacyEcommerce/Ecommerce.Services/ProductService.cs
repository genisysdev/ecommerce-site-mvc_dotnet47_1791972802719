using System.Linq;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.ViewModels;

namespace Ecommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _products;

        public ProductService(IProductRepository products)
        {
            _products = products;
        }

        public ProductListViewModel GetList(ProductFilterViewModel filter)
        {
            filter = filter ?? new ProductFilterViewModel();
            return new ProductListViewModel
            {
                Filter = filter,
                Products = _products.Query(filter).ToList()
            };
        }

        public ProductDetailViewModel GetDetail(int id)
        {
            var product = _products.GetById(id);
            if (product == null) return null;

            return new ProductDetailViewModel
            {
                Product = product,
                Images = product.Images,
                Variants = product.Variants,
                SelectedVariantId = product.Variants.FirstOrDefault()?.Id ?? 0
            };
        }
    }
}
