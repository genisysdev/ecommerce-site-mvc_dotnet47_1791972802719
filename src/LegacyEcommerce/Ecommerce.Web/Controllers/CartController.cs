using System.Web.Mvc;
using Ecommerce.Core.Interfaces;
using Ecommerce.Web.Infrastructure;

namespace Ecommerce.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Header mini-cart (rendered by the layout on every page)
        [ChildActionOnly]
        public ActionResult MiniCart()
        {
            return PartialView("_MiniCart", Session.GetCart());
        }

        // Full cart page
        public ActionResult Index()
        {
            return View(Session.GetCart());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(int productId, int quantity = 1)
        {
            var cart = _cartService.AddItem(Session.GetCart(), productId, quantity);
            Session.SaveCart(cart);
            return PartialView("_MiniCart", cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = _cartService.UpdateQuantity(Session.GetCart(), productId, quantity);
            Session.SaveCart(cart);
            return PartialView("_CartTable", cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveItem(int productId)
        {
            var cart = _cartService.RemoveItem(Session.GetCart(), productId);
            Session.SaveCart(cart);
            return PartialView("_CartTable", cart);
        }
    }
}
