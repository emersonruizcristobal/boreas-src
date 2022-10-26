using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.Shared.ViewModels {
    public class ToolStripViewModel {

        public string Label {
            get;
            set;
        }

        public ToolStripType Type {
            get;
            set;
        }

        public string PermissionClass {
            get;
            set;
        }

    }

    public enum ToolStripType {
        ForList,
        ForView
    }
}