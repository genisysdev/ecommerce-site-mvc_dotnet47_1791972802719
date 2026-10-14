using System.Web;
using Ecommerce.Core.Models;

namespace Ecommerce.Web.Infrastructure
{
    public static class CartSessionExtensions
    {
        private const string Key = "CartItems";

        public static Cart GetCart(this HttpSessionStateBase session)
        {
            var cart = session[Key] as Cart;
            if (cart == null)
            {
                cart = new Cart();
                session[Key] = cart;
            }
            return cart;
        }

        public static void SaveCart(this HttpSessionStateBase session, Cart cart)
        {
            session[Key] = cart;
        }
    }
}
