using LowercaseDashedRouting;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Areas.Home {
    public class HomeAreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "Home";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            var route = new LowercaseDashedRoute("Home/{controller}/{action}/{id}",
                                                    new RouteValueDictionary(new {
                                                        action = "Index",
                                                        id = UrlParameter.Optional
                                                    }),
                                                    new DashedRouteHandler(),
                                                    this,
                                                    context
                );
            context.Routes.Add("Home_default", route);
        }
    }
}