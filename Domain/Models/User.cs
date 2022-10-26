using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models {
    public class User : BaseModel<UserStatus> {
        public string Fullname {
            set;
            get;
        }

        public string Firstname {
            get;
            set;
        }

        public string Middlename {
            get;
            set;
        }

        public string Lastname {
            get;
            set;
        }

        public string Username {
            set;
            get;
        }

        public string Password {
            set;
            get;
        }

        public string Position {
            set;
            get;
        }
        public string Department {
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

        public Guid? ParentUser {
            get;
            set;
        }

        public string Directory {
            get;
            set;
        }

        public DateTime? DateOfBirth {
            get;
            set;
        }

        public string Barangay {
            get;
            set;
        }

        public string Gender {
            get;
            set;
        }

        public string FBId {
            get;
            set;
        }

    }
    public enum UserStatus {
        InActive,
        Active
    }
}
