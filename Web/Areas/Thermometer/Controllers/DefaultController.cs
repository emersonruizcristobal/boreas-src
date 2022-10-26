using Service.Thermometer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Areas.Shared.Controllers;
using Web.Areas.Thermometer.Data;

namespace Web.Areas.Thermometer.Controllers {
    public class DefaultController : BaseController {

        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public JsonResult Save(ThermometerViewModel viewModel) {
            try {
                new ThermometerService().Save(viewModel.Thermometer);
                return Json("Saved", JsonRequestBehavior.AllowGet);
            }catch(Exception exception) {
                return JsonError(exception.Message);
            }
        }


        [HttpPost]
        public JsonResult Delete(Guid id) {
            try {
                new ThermometerService().Delete(id);
                return Json("Deleted", JsonRequestBehavior.AllowGet);
            } catch (Exception exception) {
                return JsonError(exception.Message);
            }
        }


        [HttpGet]
        public JsonResult GetAll() {
            try {
                var data = new ThermometerService().GetAll();
                return Json(data, JsonRequestBehavior.AllowGet);
            } catch (Exception exception) {
                return JsonError(exception.Message);
            }
        }
    }
}