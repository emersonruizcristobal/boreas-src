using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Shared.ViewModels {
    public class AdministratorViewModel {
    }
    

    

    public class AdministratorEmployeeViewModel {

        public IEnumerable<SelectListItem> EmployeeTypes {
            get;
            set;
        }
        public IEnumerable<SelectListItem> EmployeeStatuses {
            get;
            set;
        }

        public Domain.Models.User User {
            get;
            set;
        }

        public IEnumerable<SelectListItem> Roles {
            get;
            set;
        }
    }

    public class AdministratorLocationViewModel {
        public IEnumerable<SelectListItem> LocationTypes {
            get;
            set;
        }

    }

}