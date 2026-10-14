using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Core.Models
{
    public class Cart
    {
        public List<CartLine> Items { get; set; } = new List<CartLine>();
        public int TotalQuantity { get { return Items.Sum(i => i.Quantity); } }
        public decimal TotalPrice { get { return Items.Sum(i => i.LineTotal); } }
    }
}
