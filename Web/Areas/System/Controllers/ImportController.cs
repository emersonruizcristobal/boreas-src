using Domain.Enums;
using Domain.Models;
using Service.Attributes;
using Service.Import;
using Web.Areas.Shared.Controllers;
using Web.Areas.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.System.Controllers {
    public class ImportController : BaseController {

        [AuthorizeRoleBase(ApplicationElement = ApplicationElement.ElementUnknown)]
        public ActionResult Index() {
            return View(new SystemImportIndexViewModel { 
                                ImportCSVViewModel  = new ImportCSVViewModel{
                                                            CSVSampleURL    = "/content/files/templates/sample-file.csv",
                                                            Title           = "Import Products",
                                                            URLEndPoint     = "/administration/import/products"
                                                        
                                                      }
                        });
        }

        [AuthorizeRoleBase(ApplicationElement = ApplicationElement.ElementUnknown)]
        public JsonResult Products(IEnumerable<HttpPostedFileBase> files) {
            try {
                return Json("Products Successfully imported");
            } catch (Exception exception) {
                return JsonError(exception.Message);
            }
        }
    }
}