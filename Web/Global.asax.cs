using Domain.Configurations;
using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Web {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine()); 

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            new ApplicationInitializer();

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}
