using LowercaseDashedRouting;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Areas.Account {
    public class AccountAreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "Account";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            var route = new LowercaseDashedRoute("Account/{controller}/{action}/{id}",
                                                    new RouteValueDictionary(new {
                                                        action  = "Index",
                                                        id      = UrlParameter.Optional
                                                    }),
                                                    new DashedRouteHandler(),
                                                    this,
                                                    context
                );
            context.Routes.Add("Account_default", route);
        }
    }
}