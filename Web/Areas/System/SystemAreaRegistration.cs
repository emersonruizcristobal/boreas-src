using LowercaseDashedRouting;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Areas.System {
    public class SystemAreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "System";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            var route = new LowercaseDashedRoute("System/{controller}/{action}/{id}",
                                                    new RouteValueDictionary(new {
                                                        action  = "Index",
                                                        id      = UrlParameter.Optional
                                                    }),
                                                    new DashedRouteHandler(),
                                                    this,
                                                    context
                );
            context.Routes.Add("System_default", route);
        }
    }
}