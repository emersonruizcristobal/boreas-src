using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models {
    public class Role : BaseModel<RoleState> {
        public string Name {
            set;
            get;
        }
        public string Description {
            set;
            get;
        }
        public RoleType RoleType {
            set;
            get;
        }
    }
    public enum RoleState {
        InActive,
        Active
    }
}
