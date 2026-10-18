using FuturePathGroup10.Data;
using FuturePathGroup10.Learner;
using FuturePathGroup10.Prerequisites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FuturePathGroup10.Presentation.Learner
{
    public partial class CaptureMarksForm : Form
    {
        private record SubjectMarkInput(ComboBox SubjectInput, NumericUpDown GradeInput);

        private readonly SubjectMarkInput[] _subjectMarkInputs;
        private readonly SubjectDB _subjectDb;
        private readonly LearnerController _learnerController;

        public CaptureMarksForm(SubjectDB subjectDB, LearnerController learnerController)
        {
            InitializeComponent();
            _subjectDb = subjectDB;
            _learnerController = learnerController;

            // Initialize grade inputs
            _subjectMarkInputs =
                [
                    new SubjectMarkInput(s1ComboBox, s1Num),
                    new SubjectMarkInput(s2ComboBox, s2Num),
                    new SubjectMarkInput(s3ComboBox, s3Num),
                    new SubjectMarkInput(s4ComboBox, s4Num),
                    new SubjectMarkInput(s5ComboBox, s5Num),
                    new SubjectMarkInput(s6ComboBox, s6Num),
                    new SubjectMarkInput(s7ComboBox, s7Num),
                    new SubjectMarkInput(s8ComboBox, s8Num),
                ];

            // Allow the user to select any subject from the database
            foreach (SubjectMarkInput input in _subjectMarkInputs)
            {
                input.SubjectInput.DataSource = subjectDB.subjects;
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Fail if no subjects were entered
                Dictionary<Subject, double> marks = GatherSubjectMarks();
                if (marks.Count == 0)
                {
                    MessageBox.Show(
                        "Please enter at least one subject and a matching mark.",
                        "No Subjects Entered",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return;
                }

                // Ask if the user wants to proceed
                DialogResult proceedResult = MessageBox.Show(
                    "Are you sure you want to submit the entered subjects and marks?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                if (proceedResult != DialogResult.Yes) { return; }

                // Fail if something goes wrong in captureMarks()
                bool success = _learnerController.captureMarks(marks, out string error);
                if (!success)
                {
                    MessageBox.Show(
                        error,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return;
                }

                // Successfully captured subjects and marks
                MessageBox.Show(
                    "Successfully captured subject marks. The form will now close.",
                    "Successfully Captured Marks",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Close();
            }
            catch (ArgumentException)
            {
                MessageBox.Show(
                    "Multiple marks were entered for the same subject. Please only enter one mark per subject.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            
        }

        private Dictionary<Subject, double> GatherSubjectMarks()
        {
            Dictionary<Subject, double> marks = [];
            foreach (SubjectMarkInput inputs in _subjectMarkInputs)
            {
                // Skip lines where subject hasn't been selected
                if (inputs.SubjectInput.SelectedItem == null) { continue; }

                marks.Add((Subject)inputs.SubjectInput.SelectedItem, (double)inputs.GradeInput.Value);
            }
            return marks;
        }
    }
}
