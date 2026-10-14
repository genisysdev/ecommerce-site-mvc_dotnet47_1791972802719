using System.Collections.Generic;
using Ecommerce.Core.Models;
using Ecommerce.Core.ViewModels;

namespace Ecommerce.Core.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> Query(ProductFilterViewModel filter);
        Product GetById(int id);
    }
}
