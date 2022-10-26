using Domain.Enums;
using Facebook;
using Service.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Areas.Shared.Controllers;

namespace Web.Areas.Home.Controllers {
    public class MainController : BaseController {
        public ActionResult Index() {
            return View();
        }


    }
}