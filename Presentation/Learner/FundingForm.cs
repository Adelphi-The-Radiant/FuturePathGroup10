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
        private FundingOption? fundingOption;
        private Reccomendation.Degree degree;
        private readonly bool fromAlternatives;

        public FundingForm(FundingOption? fundingOption, Reccomendation.Degree degree, bool fromAlternatives)
        {
            InitializeComponent();

            this.fundingOption = fundingOption;
            this.degree = degree;
            this.fromAlternatives = fromAlternatives;

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
            fundingInfoLabel.Text = info == "" ? "No funding options are listed for this qualification yet." : info;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;

            if (fromAlternatives)
            {
                parent.OpenAlternatives();
            }
            else
            {
                parent.StartAdvisoryForm();
            }
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;
            parent.OpenLearnerReport();
        }
    }
}
