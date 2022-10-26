using Domain.Enums;
using Service.Persistence;
using Service.RoleTemplate;
using System.Web;
using System.Web.Mvc;

namespace Service.Attributes {
    public class AuthorizeRoleBaseAttribute : AuthorizeAttribute {

        public ApplicationElement ApplicationElement {
            set;
            get;
        }



        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext) {
            if (!base.AuthorizeCore(httpContext))
                return false;

            if (UserSessionService<Domain.Models.User>.CurrentUser == null)
                return false;

            if (this.ApplicationElement == ApplicationElement.ElementUnknown)
                return false;

            if (httpContext.User.Identity.IsAuthenticated) {
                return new RoleTemplateService()
                                .CheckIfRoleIsAllowed(UserSessionService<Domain.Models.User>.CurrentUser.Role.Id, this.ApplicationElement);
            } else {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) {
            if (!filterContext.HttpContext.Request.IsAuthenticated)
                base.HandleUnauthorizedRequest(filterContext);

            if (UserSessionService<Domain.Models.User>.CurrentUser == null)
                base.HandleUnauthorizedRequest(filterContext);
            else
                throw new HttpException(403, "Forbidden");
        }

    }
}
