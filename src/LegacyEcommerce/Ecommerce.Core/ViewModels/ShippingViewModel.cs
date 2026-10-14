using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Core.ViewModels
{
    public class ShippingViewModel
    {
        [Required(ErrorMessage = "Please choose a shipping method.")]
        public string ShippingMethod { get; set; }

        public static readonly Dictionary<string, decimal> Options = new Dictionary<string, decimal>
        {
            { "Standard", 5.00m },
            { "Express", 15.00m }
        };

        public static decimal CostFor(string method)
        {
            decimal cost;
            return (method != null && Options.TryGetValue(method, out cost)) ? cost : 0m;
        }
    }
}
