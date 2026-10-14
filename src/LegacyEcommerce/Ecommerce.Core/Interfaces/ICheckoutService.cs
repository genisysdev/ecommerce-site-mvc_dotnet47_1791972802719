using Ecommerce.Core.Models;

namespace Ecommerce.Core.Interfaces
{
    public interface ICheckoutService
    {
        int CompleteOrder(CheckoutInfo info, Cart cart);
    }
}
