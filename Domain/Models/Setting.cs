using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models {
    public class Setting : BaseModel<SettingStatus> {
        public string Name {
            set;
            get;
        }

        public string Value {
            set;
            get;
        }

        public string Group {
            set;
            get;
        }
    }

    public enum SettingStatus {
        InActive,
        Active
    }


}
