using Domain.Enums;
using Service.Persistence;
using System.Web.Mvc;
using System.Web.Routing;

namespace Service.RoleTemplate {
    public static class RoleTemplateViewService {
        public static MvcHtmlString GetPermission(this HtmlHelper html, ApplicationElement element) {
            if (!new RoleTemplateService().CheckIfRoleIsAllowed(UserSessionService<Domain.Models.User>.CurrentUser.RoleId, element)) {
                return (element.ToString().Contains("Station")) ? new MvcHtmlString("disabled") : new MvcHtmlString("remove");
            }
            return new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString ActionLinkWithPermission(this HtmlHelper html,
                                                                    ApplicationElement element,
                                                                    string linkText,
                                                                    string action,
                                                                    string controller,
                                                                    object routeValues,
                                                                    object htmlAttributes) {
            if (new RoleTemplateService()
                        .CheckIfRoleIsAllowed(UserSessionService<Domain.Models.User>.CurrentUser.RoleId, element)) {

                TagBuilder tagBuilder = new TagBuilder("a");
                tagBuilder.InnerHtml = linkText;
                tagBuilder.Attributes["href"] = new UrlHelper(html.ViewContext.RequestContext).Action(action, controller, routeValues);
                tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

                return MvcHtmlString.Create(tagBuilder.ToString());
            }
            else {
                return new MvcHtmlString(string.Empty);
            }

        }
    }
}
