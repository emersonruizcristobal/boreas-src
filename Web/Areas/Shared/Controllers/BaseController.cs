
using Service.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Shared.Controllers {
    public class BaseController : Controller {

        protected Domain.Models.User CurrentUser() {
            return UserSessionService<Domain.Models.User>.CurrentUser;
        }

        protected JsonResult JsonError(string errorMessage) {
            HttpContext.Response.StatusCode = (Int32)HttpStatusCode.InternalServerError;
            return Json(errorMessage);
        }

        protected JsonResult JsonError(string errorMessage, int code) {
            HttpContext.Response.StatusCode = code;
            return Json(errorMessage);
        }

        
    }
}