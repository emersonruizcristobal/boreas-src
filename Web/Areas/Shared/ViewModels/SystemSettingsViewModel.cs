using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Shared.ViewModels {
    public class SystemSettingsIndexViewModel {


        public IEnumerable<SelectListItem> SchoolYearState {
            get;
            set;
        }

    }

    public class SystemSettingsEditViewModel {

    }

    public class SystemSettingsCreateViewModel {


    }
}