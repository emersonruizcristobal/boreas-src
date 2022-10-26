using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Web.Areas.Shared.ViewModels {
    public class SystemUsersIndexViewModel {
        public List<Domain.Models.User> Users { 
            get; 
            set; 
        }

        public IEnumerable<SelectListItem> Roles {
            set;
            get;
        }
    }

    public class SystemUsersCreateViewModel {
        public Domain.Models.User User {
            set;
            get;
        }
    }

    public class SystemUsersEditViewModel {
        public Domain.Models.User User {
            set;
            get;
        }
    }

    public class SystemUsersDetailsViewModel {
        public Domain.Models.User User {
            set;
            get;
        }
    }

    public class SystemUsersPasswordResetViewModel {
        public Domain.Models.User User {
            set;
            get;
        }

        public string NewPassword {
            get;
            set;
        }

        public string ConfirmNewPassword {
            get;
            set;
        }


        public string OldPassword {
            get;
            set;
        }
    }


}
