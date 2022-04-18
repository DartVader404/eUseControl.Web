using System.Web.Optimization;

namespace eUseControl.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include("~/Content/bootstrap.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/font-awesome/css").Include("~/Content/font-awesome.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/animate/css").Include("~/Content/animate.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/icon/css").Include("~/Vendor/css/pe-icon-7-stroke.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/main/css").Include("~/Vendor/css/style.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/swiper/css").Include("~/Vendor/css/swiper-bundle.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/venobox/css").Include("~/Vendor/css/venobox.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/jquery-ui/css").Include("~/Vendor/css/jquery-ui.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/dataTables-bootstrap4-min/css").Include("~/Content/DataTables/css/dataTables.bootstrap4.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/responsive-bootstrap4-min/css").Include("~/Content/DataTables/css/responsive.bootstrap4.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/nifty/css").Include("~/Vendor/css/component.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/icofont/css").Include("~/Vendor/css/icofont.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/admin-main/css").Include("~/Vendor/css/admin-style.css", new CssRewriteUrlTransform()));

            bundles.Add(new Bundle("~/bundles/bootstrap-bundle-min/js").Include("~/Scripts/bootstrap.bundle.min.js"));
            bundles.Add(new Bundle("~/bundles/bootstrap-min/js").Include("~/Scripts/bootstrap.min.js"));
            bundles.Add(new Bundle("~/bundles/jquery-3.6.0/js").Include("~/Scripts/jquery-3.6.0.min.js"));
            bundles.Add(new Bundle("~/bundles/jquery-migrate/js").Include("~/Vendors/js/jquery-migrate-3.3.2.min.js"));
            bundles.Add(new Bundle("~/bundles/modernizr/js").Include("~/Vendors/js/modernizr-3.11.2.min"));
            bundles.Add(new Bundle("~/bundles/jquery-countdown/js").Include("~/Vendors/js/jquery.countdown.min.js"));
            bundles.Add(new Bundle("~/bundles/swiper/js").Include("~/Vendors/js/swiper-bundle.min.js"));
            bundles.Add(new Bundle("~/bundles/scrollUp/js").Include("~/Vendors/js/scrollUp.js"));
            bundles.Add(new Bundle("~/bundles/venobox/js").Include("~/Vendors/js/venobox.min.js"));
            bundles.Add(new Bundle("~/bundles/jquery-ui/js").Include("~/Vendors/js/jquery-ui.min.js"));
            bundles.Add(new Bundle("~/bundles/mailchimp-ajax/js").Include("~/Vendors/js/mailchimp-ajax.js"));
            bundles.Add(new Bundle("~/bundles/main/js").Include("~/Vendors/js/main.js"));
            bundles.Add(new Bundle("~/bundles/popper/js").Include("~/Vendor/js/popper.min.js"));
            bundles.Add(new Bundle("~/bundles/jquery/js").Include("~/Scripts/jquery.min.js"));
            bundles.Add(new Bundle("~/bundles/jquery-slimscroll/js").Include("~/Scripts/jquery.slimscroll.js"));
            bundles.Add(new Bundle("~/bundles/jquery-dataTables-min/js").Include("~/Scripts/DataTables/jquery.dataTables.min.js"));
            bundles.Add(new Bundle("~/bundles/dataTables-bootstrap4-min/js").Include("~/Scripts/DataTables/dataTables.bootstrap4.min.js"));
            bundles.Add(new Bundle("~/bundles/dataTables-responsive-min/js").Include("~/Scripts/DataTables/dataTables.responsive.min.js"));
            bundles.Add(new Bundle("~/bundles/responsive-bootstrap4-min/js").Include("~/Scripts/DataTables/responsive.bootstrap4.min.js"));
            bundles.Add(new Bundle("~/bundles/classie/js").Include("~/Vendor/js/classie.js"));
            bundles.Add(new Bundle("~/bundles/modalEffects/js").Include("~/Vendor/js/modalEffects.js"));
            bundles.Add(new Bundle("~/bundles/product-list/js").Include("~/Vendor/js/product-list.js"));
        }
    }
}