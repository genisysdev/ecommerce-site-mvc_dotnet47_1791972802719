using System.Linq;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;

namespace Ecommerce.Services
{
    public class CartService : ICartService
    {
        private readonly IProductRepository _products;

        public CartService(IProductRepository products)
        {
            _products = products;
        }

        public Cart AddItem(Cart cart, int productId, int quantity)
        {
            if (quantity < 1) quantity = 1;
            var line = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (line != null)
            {
                line.Quantity += quantity;
                return cart;
            }

            var product = _products.GetById(productId);
            if (product == null) return cart; // unknown product -> no-op

            cart.Items.Add(new CartLine
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = quantity
            });
            return cart;
        }

        public Cart UpdateQuantity(Cart cart, int productId, int quantity)
        {
            var line = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (line == null) return cart;
            if (quantity <= 0)
                cart.Items.Remove(line);
            else
                line.Quantity = quantity;
            return cart;
        }

        public Cart RemoveItem(Cart cart, int productId)
        {
            var line = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (line != null) cart.Items.Remove(line);
            return cart;
        }
    }
}
