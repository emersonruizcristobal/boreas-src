using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Error.Controllers {
    public class HttpController : Controller {
        [Authorize]
        public ActionResult code403() {
            return View();
        }

        [Authorize]
        public ActionResult code404() {
            return View();
        }

        [Authorize]
        public ActionResult code500() {
            return View();
        }
    }
}