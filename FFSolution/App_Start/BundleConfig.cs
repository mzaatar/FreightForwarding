using System.Web;
using System.Web.Optimization;

namespace FFSolution
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            /*
            <script src="/Scripts/jquery-2.1.3.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.dataTables.js" type="text/javascript"></script>
            */
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-2.1.3.min.js",
                       "~/Scripts/jquery-2.1.3.js",
                       "~/Scripts/jquery-ui.js",
                       "~/Scripts/jquery.dataTables.js",
                       "~/Scripts/dataTables.tableTools.js",
                       "~/Scripts/jquery.blockUI.js",
                       "~/Scripts/jquery.remodal.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/datatablecss").Include(
                      "~/Content/jquery.dataTables.css",
                      "~/Content/dataTables.tableTools.css",
                      "~/Content/jquery.remodal.css"));

            //bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
            //         "~/Scripts/bootstrap-datepicker.js"));
        }
    }
}
