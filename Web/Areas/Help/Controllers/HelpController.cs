using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Areas.Shared.Controllers;

namespace Web.Areas.Help.Controllers {
    public class HelpController : BaseController {
        public ActionResult Index() {
            return View();
        }
    }
}