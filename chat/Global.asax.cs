using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using chat.App_Start;

namespace chat
{
    public class MvcApplication : HttpApplication
    {


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteTable.Routes.MapHubs();
            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}