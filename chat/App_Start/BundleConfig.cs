using System.Web.Optimization;

namespace chat.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/styles").Include(
                        "~/Content/bootstrap.min.css").Include(
                        "~/Content/bootstrap-responsive.min.css").Include(
                        "~/Content/1111191211.css"));

            bundles.Add(
                new ScriptBundle("~/bundles/js")
                .Include("~/Scripts/jquery-{version}.min.js")
                .Include("~/Scripts/bootstrap.min.js")
                .Include("~/Scripts/jquery.signalR-{version}.min.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/control")
                .Include("~/Scripts/bootbox.min.js")
                .Include("~/Scripts/shortcut.js")
                .Include("~/Scripts/controls.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}