using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.Shared.ViewModels {
    public class SystemRolesIndexViewModel {
        public List<Domain.Models.Role> Roles {
            set;
            get;
        }
    }

    public class SystemRolesCreateViewModel {
        public Domain.Models.Role Role {
            set;
            get;
        }
    }

    public class SystemRolesEditViewModel {
        public Domain.Models.Role Role {
            set;
            get;
        }
    }

    public class SystemRolesViewViewModel {

        public Domain.Models.Role Role {
            set;
            get;
        }

        public Dictionary<string, Domain.Models.RoleTemplate> RoleTemplates { 
            get; 
            set; 
        }
    }

    public class SystemRolesGrantPermissionViewModel {

        public Domain.Models.RoleTemplate RoleTemplate {
            set;
            get;
        }
    }
}