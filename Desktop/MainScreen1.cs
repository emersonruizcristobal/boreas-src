using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop {
    public partial class MainScreen1 : Form {
        public MainScreen1() {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            var confirmResult = MessageBox.Show("Are you sure to close this application?",
                                     "Confirm Exit!!",
                                     MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes) {
                this.Close();
            } else {
                return;
            }
        }
    }
}
