using System.Web.Optimization;

namespace Ecommerce.Web
{
    public static class BundleConfig
    {
        // Bundles are registered for parity with the legacy setup. The layout
        // currently references scripts directly; bundling can be switched on later.
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Site.css"));
        }
    }
}
