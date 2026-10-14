using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Core.ViewModels
{
    public class PaymentViewModel
    {
        [Required, StringLength(100)]
        [Display(Name = "Name on card")]
        public string CardName { get; set; }

        [Required, CreditCard]
        [Display(Name = "Card number")]
        public string CardNumber { get; set; }

        [Required]
        [RegularExpression(@"^(0[1-9]|1[0-2])/\d{2}$", ErrorMessage = "Use MM/YY")]
        public string Expiry { get; set; }

        [Required]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "3-4 digits")]
        [Display(Name = "CVV")]
        public string Cvv { get; set; }
    }
}
