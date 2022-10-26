using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Areas.Shared.ViewModels;

namespace Web.Areas.Settings.Models {
    public class SettingsViewModel : BaseViewModel {

        public bool BackgroundIsOn {
            get;
            set;
        }

        public int Start {
            get;
            set;
        }

        public int End {
            get;
            set;
        }

        public string Prefix {
            get;
            set;
        }

        public string BankName {
            get;
            set;
        }

        public string Property {
            get;
            set;
        }

        public Domain.Models.Setting Setting {
            get;
            set;
        }

    }
}