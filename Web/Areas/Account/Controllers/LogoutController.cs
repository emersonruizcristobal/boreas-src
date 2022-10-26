using Service.Persistence;
using Web.Areas.Shared.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Notifications;

namespace Web.Areas.Account.Controllers {
    public class LogoutController : BaseController {
        public ActionResult Index() {
            UserSessionService<Domain.Models.User>.End();
            return RedirectToAction("Index", "Login", new { Area = "Account" }).Success("You have successfully logged out.");
        }
    }
}