using System.Web.Optimization;

namespace Geo.Localization.WebApp
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            bundles.UseCdn = true;


            bundles.Add(new ScriptBundle("~/bundles/base").Include(
                "~/js/base/ScriptBase.js",
                "~/js/controllers/AccountComponent.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/js/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/lang").Include(
                "~/js/lang/multiLanguajeMVC.js",
                "~/js/lang/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/js/angular.min.js",
                "~/js/ui-bootstrap-tpls-1.3.3.min.js",
                "~/js/bootstrap-angular-validation-all.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/js/validate/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/js/bootstrap.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/css/bootstrap.css",
                "~/css/modern-business.css"));

        }
    }
}