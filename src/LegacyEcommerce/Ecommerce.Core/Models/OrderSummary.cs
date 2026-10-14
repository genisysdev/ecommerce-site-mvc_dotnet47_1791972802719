using System;

namespace Ecommerce.Core.Models
{
    public class OrderSummary
    {
        public int OrderId { get; set; }
        public DateTime CreatedUtc { get; set; }
        public decimal Total { get; set; }
    }
}
