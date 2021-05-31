using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beijersbergen.LabelManager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void generateQRCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = new FluidUserControl()
            {
                Dock = DockStyle.Fill
            };
            mainPanel.Controls.Add(control);
        }

        private void printZebraLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = new GseUserControl()
            {
                Dock = DockStyle.Fill
            };
            mainPanel.Controls.Add(control);
        }
    }
}
