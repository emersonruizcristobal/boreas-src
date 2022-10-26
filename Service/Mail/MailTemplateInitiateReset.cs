using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mail {
    public class MailTemplateInitiateReset : Postal.Email {
        public string To {
            set;
            get;
        }

        public string From {
            set;
            get;
        }

        public string Name {
            set;
            get;
        }

        public string Link {
            set;
            get;
        }
    }
}
