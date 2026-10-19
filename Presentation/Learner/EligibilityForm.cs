using System.Linq;
using FuturePathGroup10.Learner;
using FuturePathGroup10.Business.Prerequisite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FuturePathGroup10.Presentation.Learner
{
    public partial class EligibilityForm : Form
    {
        private readonly LearnerController _learnerController;

        public EligibilityForm(LearnerController learnerController)
        {
            InitializeComponent();
            _learnerController = learnerController;

            ShowEligibility();
        }

        private void backButton_Click(object? sender, EventArgs e)
        {
            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;
            parent.OpenQualificationScreen();
        }

        private void alternativeButton_Click(object? sender, EventArgs e)
        {
            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;
            parent.OpenAlternatives();
        }

        private void fundButton_Click(object? sender, EventArgs e)
        {
            if (_learnerController.SelectedDegree is null)
            {
                MessageBox.Show(
                    "Funding options are shown for a qualification. Go back and select a qualification to see its funding options.",
                    "No Qualification Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;
            parent.OpenFunding(_learnerController.SelectedDegree, false);
        }

        private void reportButton_Click(object? sender, EventArgs e)
        {
            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;
            parent.OpenLearnerReport();
        }

        private void ShowEligibility()
        {
            List<string> selected = [];
            if (_learnerController.SelectedDegree is not null)
            {
                selected.Add("Qualification: " + _learnerController.SelectedDegree.title);
            }
            if (_learnerController.SelectedFaculty is not null)
            {
                selected.Add("Faculty: " + _learnerController.SelectedFaculty.title);
            }
            selectionLabel.Text = string.Join("  |  ", selected);

            List<EligibilityResult> results = _learnerController.checkSelectionEligibility(out string error);

            if (results.Count == 0)
            {
                ShowOutcome(AdvisoryOutcome.MoreInformationNeeded);
                detailsTextBox.Text = string.IsNullOrEmpty(error)
                    ? "No qualifications are listed for this faculty yet." + Environment.NewLine + Environment.NewLine + EligibilityResult.Disclaimer
                    : error;
                return;
            }

            ShowOutcome(results.Min(r => r.Outcome));

            StringBuilder details = new();
            if (results.Count > 1)
            {
                details.AppendLine("Your eligibility for each qualification in this faculty:");
                foreach (EligibilityResult result in results)
                {
                    details.AppendLine("- " + result.DegreeTitle + ": " + OutcomeText(result.Outcome));
                }
            }

            details.AppendLine("Select View Report to see the reasons behind this result and what could strengthen your application.");
            details.AppendLine();
            details.Append(EligibilityResult.Disclaimer);
            detailsTextBox.Text = details.ToString().Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
        }

        private void ShowOutcome(AdvisoryOutcome outcome)
        {
            statusLabel.Text = OutcomeText(outcome);

            switch (outcome)
            {
                case AdvisoryOutcome.StrongMatch:
                    eligibilityLabel.Text = "You're Eligible!";
                    eligibleSubtitleLabel.Text = "Congratulations, you meet the requirements.";
                    break;
                case AdvisoryOutcome.PossibleMatch:
                    eligibilityLabel.Text = "You May Be Eligible";
                    eligibleSubtitleLabel.Text = "You're close. See what could strengthen your application.";
                    break;
                case AdvisoryOutcome.NotCurrentlyEligible:
                    eligibilityLabel.Text = "Not Currently Eligible";
                    eligibleSubtitleLabel.Text = "Don't give up. Have a look at the alternative options.";
                    break;
                default:
                    eligibilityLabel.Text = "More Information Needed";
                    eligibleSubtitleLabel.Text = "We could not complete the check yet.";
                    break;
            }
        }

        private static string OutcomeText(AdvisoryOutcome outcome)
        {
            return outcome switch
            {
                AdvisoryOutcome.StrongMatch => "Strong Match",
                AdvisoryOutcome.PossibleMatch => "Possible Match",
                AdvisoryOutcome.NotCurrentlyEligible => "Not Currently Eligible",
                _ => "More Information Needed"
            };
        }
    }
}
