using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using Ecommerce.Core.ViewModels;
using Ecommerce.Web.Infrastructure;
using Ecommerce.Web.Models;

namespace Ecommerce.Web.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        private bool CartEmpty()
        {
            return Session.GetCart().Items.Count == 0;
        }

        // Step 1: Address
        public ActionResult Address()
        {
            if (CartEmpty()) return RedirectToAction("Index", "Cart");
            var info = Session.GetCheckoutInfo();
            var vm = info.Address ?? new AddressViewModel();

            // Prefill from the signed-in account so the order links to it (by email).
            if (info.Address == null && User.Identity.IsAuthenticated)
            {
                vm.Email = User.Identity.Name;
                var mgr = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = mgr.FindById(User.Identity.GetUserId());
                if (user != null) vm.FullName = user.FullName;
            }
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Address(AddressViewModel model)
        {
            if (CartEmpty()) return RedirectToAction("Index", "Cart");
            if (!ModelState.IsValid) return View(model);
            var info = Session.GetCheckoutInfo();
            info.Address = model;
            Session.SaveCheckoutInfo(info);
            return RedirectToAction("Shipping");
        }

        // Step 2: Shipping
        public ActionResult Shipping()
        {
            if (CartEmpty()) return RedirectToAction("Index", "Cart");
            var info = Session.GetCheckoutInfo();
            if (info.Address == null) return RedirectToAction("Address");
            return View(new ShippingViewModel { ShippingMethod = info.ShippingMethod });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Shipping(ShippingViewModel model)
        {
            if (CartEmpty()) return RedirectToAction("Index", "Cart");
            var info = Session.GetCheckoutInfo();
            if (info.Address == null) return RedirectToAction("Address");
            if (!ModelState.IsValid) return View(model);
            info.ShippingMethod = model.ShippingMethod;
            info.ShippingCost = ShippingViewModel.CostFor(model.ShippingMethod);
            Session.SaveCheckoutInfo(info);
            return RedirectToAction("Payment");
        }

        // Step 3: Payment
        public ActionResult Payment()
        {
            if (CartEmpty()) return RedirectToAction("Index", "Cart");
            var info = Session.GetCheckoutInfo();
            if (info.Address == null) return RedirectToAction("Address");
            if (string.IsNullOrEmpty(info.ShippingMethod)) return RedirectToAction("Shipping");
            SetTotals(info);
            return View(new PaymentViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Payment(PaymentViewModel model)
        {
            if (CartEmpty()) return RedirectToAction("Index", "Cart");
            var info = Session.GetCheckoutInfo();
            if (info.Address == null) return RedirectToAction("Address");
            if (string.IsNullOrEmpty(info.ShippingMethod)) return RedirectToAction("Shipping");
            if (!ModelState.IsValid)
            {
                SetTotals(info);
                return View(model);
            }

            var cart = Session.GetCart();
            var total = cart.TotalPrice + info.ShippingCost;
            var orderId = _checkoutService.CompleteOrder(info, cart);

            Session.SaveCart(new Cart());   // clear cart
            Session.ClearCheckout();        // clear wizard state
            TempData["ConfirmTotal"] = total;
            return RedirectToAction("Confirmation", new { orderId });
        }

        // Step 4: Confirmation
        public ActionResult Confirmation(int orderId)
        {
            return View(new ConfirmationViewModel
            {
                OrderId = orderId,
                Total = TempData["ConfirmTotal"] != null ? (decimal)TempData["ConfirmTotal"] : 0m
            });
        }

        private void SetTotals(CheckoutInfo info)
        {
            var cartTotal = Session.GetCart().TotalPrice;
            ViewBag.CartTotal = cartTotal;
            ViewBag.ShippingCost = info.ShippingCost;
            ViewBag.GrandTotal = cartTotal + info.ShippingCost;
        }
    }
}
