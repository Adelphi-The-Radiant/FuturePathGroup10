using System.IO;
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
    public partial class LearnerReportForm : Form
    {
        private readonly LearnerController _learnerController;

        public LearnerReportForm(LearnerController learnerController)
        {
            InitializeComponent();
            _learnerController = learnerController;

            summaryTextBox.Text = BuildReport();
            summaryTextBox.Select(0, 0);
        }

        private void backButton_Click(object? sender, EventArgs e)
        {
            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;
            parent.StartAdvisoryForm();
        }

        private void exportButton_Click(object? sender, EventArgs e)
        {
            string name = _learnerController.activeLearner?.Username;
            string fileName = "FuturePath_Report" + (string.IsNullOrWhiteSpace(name) ? "" : "_" + name.Trim()) + ".txt";
            foreach (char invalid in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(invalid, '_');
            }

            saveFileDialog.FileName = fileName;
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveFileDialog.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                File.WriteAllText(saveFileDialog.FileName, summaryTextBox.Text, Encoding.UTF8);
                MessageBox.Show(
                    "Report saved to " + saveFileDialog.FileName,
                    "Report Exported",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not save the report: " + ex.Message,
                    "Export Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private string BuildReport()
        {
            StringBuilder report = new();

            if (_learnerController.SelectedDegree is not null)
            {
                report.AppendLine("Qualification: " + _learnerController.SelectedDegree.title);
            }
            if (_learnerController.SelectedFaculty is not null)
            {
                report.AppendLine("Faculty: " + _learnerController.SelectedFaculty.title);
            }
            report.AppendLine();

            List<EligibilityResult> results = _learnerController.checkSelectionEligibility(out string error);

            if (results.Count == 0)
            {
                report.AppendLine(string.IsNullOrEmpty(error)
                    ? "No qualifications are listed for this faculty yet."
                    : error);
            }

            foreach (EligibilityResult result in results)
            {
                report.AppendLine(result.DegreeTitle + " - " + OutcomeText(result.Outcome));
                report.AppendLine(result.Summary);

                if (result.Reasons.Count > 0)
                {
                    report.AppendLine();
                    report.AppendLine("Why:");
                    foreach (string reason in result.Reasons)
                    {
                        report.AppendLine("- " + reason);
                    }
                }

                if (result.InterestMatch.HasValue)
                {
                    report.AppendLine("- Interest alignment: " + Math.Round(result.InterestMatch.Value) + "%");
                }

                report.AppendLine();
            }

            report.Append(EligibilityResult.Disclaimer);
            return report.ToString();
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
