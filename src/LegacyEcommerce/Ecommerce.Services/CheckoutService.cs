using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;

namespace Ecommerce.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IOrderRepository _orders;

        public CheckoutService(IOrderRepository orders)
        {
            _orders = orders;
        }

        public int CompleteOrder(CheckoutInfo info, Cart cart)
        {
            var total = cart.TotalPrice + info.ShippingCost;
            return _orders.PlaceOrder(info.Address.Email, info.Address.FullName, total);
        }
    }
}
