using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.Shared.ViewModels {
    public class ImportCSVViewModel {
        public string Title {
            set;
            get;
        }

        public string CSSClass {
            set;
            get;
        }

        public string URLEndPoint {
            set;
            get;
        }

        public string CSVSampleURL {
            set;
            get;
        }
    }
}