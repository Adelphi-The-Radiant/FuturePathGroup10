using FuturePathGroup10.Advisory;
using FuturePathGroup10.Funding;
using FuturePathGroup10.Data;
using FuturePathGroup10.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FuturePathGroup10.Data;

namespace FuturePathGroup10.Presentation.Learner
{
    public partial class FundingForm : Form
    {
        private FundingOption fundingOption;
        private Reccomendation.Degree degree;

        public FundingForm(FundingOption fundingOption, Reccomendation.Degree degree)
        {
            InitializeComponent();

            this.fundingOption = fundingOption;
            this.degree = degree;

            displayAll();
        }

        private void displayAll()
        {
            selectionButton.Text = degree.title;

            string info = "";
            foreach (var fund in degree.options)
            {
                info += fund.optionToString() + "\n";
            }
            fundingInfoLabel.Text = info;
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
    }
}
