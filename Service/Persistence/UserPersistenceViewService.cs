using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Service.Persistence {
    public static class PersistenceViewHelper {
        public static MvcHtmlString GreetLoggedUser(this HtmlHelper html) {
            Domain.Models.User CurrentUser = UserSessionService<Domain.Models.User>.CurrentUser;
            string[] greetings = {
                                 ";)",
                                 "Good day to you, {0}?", 
                                 "Magandang araw, {0}.", 
                                 "How's your day, {0}?",
                                 "Heeyah, {0}"
                                };

            return new MvcHtmlString(String.Format(greetings[new Random().Next(greetings.Length)], 
                                                   CurrentUser.Fullname));
        }

        public static MvcHtmlString GetLoggedUserFullName(this HtmlHelper html) {
            return new MvcHtmlString(UserSessionService<Domain.Models.User>.CurrentUser.Fullname);
        }

       

        public static MvcHtmlString GetLoggedUserRole(this HtmlHelper html) {
            return new MvcHtmlString(UserSessionService<Domain.Models.User>.CurrentUser.Role.Name);
        }

    }
}