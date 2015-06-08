namespace Errs.WebUi
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
               "~/Scripts/jquery-{version}.js",
               "~/Scripts/bootstrap.js",
               "~/Scripts/jquery.validate.js",
               "~/scripts/jquery.validate.unobtrusive.js"
               ));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/app.css"));
        }
    }
}