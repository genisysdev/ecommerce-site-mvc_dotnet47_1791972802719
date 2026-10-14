using System.Collections.Generic;
using Ecommerce.Core.Models;

namespace Ecommerce.Core.ViewModels
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public ProductFilterViewModel Filter { get; set; } = new ProductFilterViewModel();
    }
}
