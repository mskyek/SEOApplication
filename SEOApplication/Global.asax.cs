using Serilog;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SEOApplication
{
    public class MvcApplication : HttpApplication
    {

        public MvcApplication()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .CreateLogger();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
