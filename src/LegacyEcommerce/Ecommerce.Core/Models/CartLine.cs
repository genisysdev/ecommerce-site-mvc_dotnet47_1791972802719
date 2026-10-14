namespace Ecommerce.Core.Models
{
    public class CartLine
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get { return Price * Quantity; } }
    }
}
