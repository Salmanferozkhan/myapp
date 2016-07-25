using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(AttandencePortal.App_Start.BootstrapBundleConfig), "RegisterBundles")]

namespace AttandencePortal.App_Start
{
    public class BootstrapBundleConfig
    {
        
        public static void RegisterBundles()
        {
            // Add @Styles.Render("~/Content/bootstrap") in the <head/> of your _Layout.cshtml view
            // For Bootstrap theme add @Styles.Render("~/Content/bootstrap-theme") in the <head/> of your _Layout.cshtml view
            // Add @Scripts.Render("~/bundles/bootstrap") after jQuery in your _Layout.cshtml view
            // When <compilation debug="true" />, MVC4 will render the full readable version. When set to <compilation debug="false" />, the minified version will be rendered automatically
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/materialjs").Include("~/Content/material.js"));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css"));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap-theme").Include("~/Content/bootstrap-theme.css"));
            BundleTable.Bundles.Add(new StyleBundle("~/bundles/css").Include("~/Content/bootstrap.min.css", "~/Content/font-awesome.min.css","~/fonts/css.css"));
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-1.10.2.min.js", "~/Scripts/angular.js", "~/Scripts/angular-resource.js",
                "~/Scripts/angular-route.min.js", "~/Scripts/angular-animate.js", "~/Scripts/ui-bootstrap-tpls.js","~/Scripts/bootstrap.min.js", "~/Scripts/ui-bootstrap-tpls-1.3.3.js"));
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/angular").Include("~/JS/myApp.js", "~/JS/HomeCtrl.js", "~/JS/Employee.js", "~/JS/EmpStats.js", "~/Scripts/timepickerpop.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
