using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FuturePathGroup10.Presentation.Learner
{
    public partial class AlternativesForm : Form
    {
        public AlternativesForm()
        {
            InitializeComponent();


        }

        private void backButton_Click(object sender, EventArgs e)
        {
            HomeForm hf = new HomeForm();
            hf.Show();
            this.Close();
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            LearnerReportForm rf = new LearnerReportForm();
            rf.Show();
            this.Close();
        }

        private void fundButton_Click(object sender, EventArgs e)
        {
            FundingForm ff = new FundingForm();
            ff.Show();
            this.Close();
        }
    }
}
