using FuturePathGroup10.Presentation.Learner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FuturePathGroup10.Presentation
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void fundsButton_Click(object sender, EventArgs e)
        {

        }

        private void explainLabel_Click(object sender, EventArgs e)
        {

        }

        private void marksButton_Click(object sender, EventArgs e)
        {
            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;
            parent.OpenMarksCapture();
        }
    }
}
