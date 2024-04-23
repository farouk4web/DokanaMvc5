using System.Web;
using System.Web.Optimization;

namespace Dokana
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new ScriptBundle("~/js/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/js/bootbox").Include(
                      "~/Scripts/bootbox.js"));

            bundles.Add(new ScriptBundle("~/js/editor").Include(
                      "~/Scripts/tinymce/tinymce.js"));



            // Custom javascript files
            bundles.Add(new ScriptBundle("~/js/site").Include(
                      "~/Scripts/Custom/site.js"));

            bundles.Add(new ScriptBundle("~/js/stylesMode").Include(
                      "~/Scripts/Custom/stylesMode.js"));

            bundles.Add(new ScriptBundle("~/js/productActions").Include(
                      "~/Scripts/Custom/productActions.js"));

            bundles.Add(new ScriptBundle("~/js/favoritesActions").Include(
                      "~/Scripts/Custom/favoritesActions.js"));

            bundles.Add(new ScriptBundle("~/js/cartActions").Include(
                      "~/Scripts/Custom/cartActions.js"));

            bundles.Add(new ScriptBundle("~/js/productForm").Include(
                      "~/Scripts/Custom/productForm.js"));

            bundles.Add(new ScriptBundle("~/js/checkout").Include(
                      "~/Scripts/Custom/checkout.js"));

            bundles.Add(new ScriptBundle("~/js/orderActions").Include(
                      "~/Scripts/Custom/orderActions.js"));

            bundles.Add(new ScriptBundle("~/js/categoryActions").Include(
                      "~/Scripts/Custom/categoryActions.js"));



            // next lines are to load css filesused in this site
            // global css props
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/Custom/main.css"));

            bundles.Add(new StyleBundle("~/Content/css_rtl").Include(
                        "~/Content/bootstrap-rtl.css",
                        "~/Content/Custom/main.css",
                        "~/Content/Custom/main-rtl.css"));

            bundles.Add(new StyleBundle("~/fontAwesome").Include(
                        "~/Content/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/home").Include(
                        "~/Content/custom/home.css"));

            bundles.Add(new StyleBundle("~/siteForm").Include(
                        "~/Content/Custom/siteForm.css"));

            bundles.Add(new StyleBundle("~/productDetails").Include(
                        "~/Content/Custom/productDetails.css"));

            bundles.Add(new StyleBundle("~/orderForm").Include(
                        "~/Content/Custom/orderForm.css"));

            bundles.Add(new StyleBundle("~/allOrders").Include(
                        "~/Content/Custom/allOrders.css"));

            bundles.Add(new StyleBundle("~/orderDetails").Include(
                        "~/Content/Custom/orderDetails.css"));

            bundles.Add(new StyleBundle("~/checkout").Include(
                        "~/Content/Custom/checkout.css"));

            // control panel css files
            bundles.Add(new StyleBundle("~/cPanel").Include(
                        "~/Content/Custom/cPanel.css"));

            bundles.Add(new StyleBundle("~/dashboard").Include(
                        "~/Content/Custom/dashboard.css"));
        }
    }
}
