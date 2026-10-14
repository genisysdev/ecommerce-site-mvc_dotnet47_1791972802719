using System.Web;
using Ecommerce.Core.Models;

namespace Ecommerce.Web.Infrastructure
{
    public static class CheckoutSessionExtensions
    {
        private const string Key = "Checkout";

        public static CheckoutInfo GetCheckoutInfo(this HttpSessionStateBase session)
        {
            var info = session[Key] as CheckoutInfo;
            if (info == null)
            {
                info = new CheckoutInfo();
                session[Key] = info;
            }
            return info;
        }

        public static void SaveCheckoutInfo(this HttpSessionStateBase session, CheckoutInfo info)
        {
            session[Key] = info;
        }

        public static void ClearCheckout(this HttpSessionStateBase session)
        {
            session.Remove(Key);
        }
    }
}
