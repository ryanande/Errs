namespace Errs.WebUi
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/app.css"));

            bundles.Add(Foundation.Styles());
            bundles.Add(Foundation.Scripts());
        }
    }
}