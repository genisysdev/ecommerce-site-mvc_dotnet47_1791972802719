using Ecommerce.Core.ViewModels;

namespace Ecommerce.Core.Models
{
    public class CheckoutInfo
    {
        public AddressViewModel Address { get; set; }
        public string ShippingMethod { get; set; }
        public decimal ShippingCost { get; set; }
    }
}
