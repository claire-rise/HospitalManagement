using System.Web;
using System.Web.Optimization;

namespace hospitalwebapp
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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/hospital-admin.js",
                      "~/Content/hospital-admin2.js",
                      "~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/Content/build/js").Include(
                  "~/Content/build/js/custom.min.js",
                  "~/Content/build/js/custom.js"));
            bundles.Add(new ScriptBundle("~/Content/build/production/js").Include(
                  "~/Content/build/production/js/daterangepicker.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                       "~/Content/font-awesome.min.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/hospital-admin.css",
                      "~/Content/hospital-admin3.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/build/css").Include(
                     "~/Content/build/css/custom.css",
                    "~/Content/build/css/custom.min.css"));
            bundles.Add(new StyleBundle("~/Content/build/production/css/maps").Include(
                     "~/Content/build/production/css/maps/jquery-jvectormap-2.0.3.css"));
        }
    }
}
