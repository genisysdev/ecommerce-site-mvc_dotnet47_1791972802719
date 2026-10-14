using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Core.ViewModels
{
    public class AddressViewModel
    {
        [Required, StringLength(100)]
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Required, EmailAddress, StringLength(256)]
        public string Email { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Address")]
        public string AddressLine { get; set; }

        [Required, StringLength(100)]
        public string City { get; set; }

        [Required, StringLength(20)]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        [Required, StringLength(100)]
        public string Country { get; set; }
    }
}
