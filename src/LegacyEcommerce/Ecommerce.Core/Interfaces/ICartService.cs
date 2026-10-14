using Ecommerce.Core.Models;

namespace Ecommerce.Core.Interfaces
{
    public interface ICartService
    {
        Cart AddItem(Cart cart, int productId, int quantity);
        Cart UpdateQuantity(Cart cart, int productId, int quantity);
        Cart RemoveItem(Cart cart, int productId);
    }
}
