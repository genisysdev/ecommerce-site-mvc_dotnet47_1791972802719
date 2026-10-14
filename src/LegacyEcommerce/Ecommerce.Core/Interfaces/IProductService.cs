using Ecommerce.Core.ViewModels;

namespace Ecommerce.Core.Interfaces
{
    public interface IProductService
    {
        ProductListViewModel GetList(ProductFilterViewModel filter);
        ProductDetailViewModel GetDetail(int id);
    }
}
