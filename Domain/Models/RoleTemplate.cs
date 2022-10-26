using Domain.Enums;
using System;

namespace Domain.Models {

    public class RoleTemplate : BaseModel<RoleTemplateState> {

        public ApplicationElement ApplicationElement {
            set;
            get;
        }

        public Guid RoleId {
            set;
            get;
        }

        public Role Role {
            set;
            get;
        }
    }

    public enum RoleTemplateState {

    }
}
