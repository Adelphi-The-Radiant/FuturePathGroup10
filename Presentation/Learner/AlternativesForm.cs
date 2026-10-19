using FuturePathGroup10.Business.Prerequisite;
using FuturePathGroup10.Business;
using FuturePathGroup10.Data;
using FuturePathGroup10.Learner;
using FuturePathGroup10.Prerequisites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FuturePathGroup10.Presentation.Learner
{
    public partial class AlternativesForm : Form
    {
        private readonly LearnerController _learnerController;
        private readonly List<EligibilityResult> _alternatives;

        public AlternativesForm(LearnerController learnerController)
        {
            InitializeComponent();
            _learnerController = learnerController;


            _alternatives = _learnerController.getAlternatives();
            display();
        }

        private void display()
        {
            if (_alternatives.Count == 0)
            {
                listLabel.Text = "No other qualifications match your marks at the moment.";
                return;
            }

            StringBuilder output = new();
            foreach (EligibilityResult alternative in _alternatives)
            {
                output.AppendLine("- " + alternative.DegreeTitle + ": " +
                    (alternative.Outcome == AdvisoryOutcome.StrongMatch ? "Strong Match" : "Possible Match"));
            }
            listLabel.Text = output.ToString();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;
            parent.StartAdvisoryForm();
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;
            parent.OpenLearnerReport();
        }

        private void fundButton_Click(object sender, EventArgs e)
        {
            FuturePathGroup10.Reccomendation.Degree? best = _alternatives.Count == 0
                ? null
                : _learnerController.degreeDB.degrees.FirstOrDefault(d => d.title == _alternatives[0].DegreeTitle);

            if (best is null)
            {
                MessageBox.Show(
                    "There are no alternative qualifications to show funding options for.",
                    "No Alternatives",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;
            parent.OpenFunding(best, true);
        }
    }
}
