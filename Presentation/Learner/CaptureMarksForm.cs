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
        private record SubjectGradeInput(ComboBox SubjectInput, NumericUpDown GradeInput);

        private readonly SubjectGradeInput[] _subjectGradeInputs;
        private readonly Data.SubjectDB _subjectDb;
        private readonly FuturePathGroup10.Learner.Learner _learner;

        public CaptureMarksForm(Data.SubjectDB subjectDB, FuturePathGroup10.Learner.Learner learner)
        {
            InitializeComponent();
            _subjectDb = subjectDB;
            _learner = learner;

            // Initialize grade inputs
            _subjectGradeInputs =
                [
                    new SubjectGradeInput(s1ComboBox, s1Num),
                    new SubjectGradeInput(s2ComboBox, s2Num),
                    new SubjectGradeInput(s3ComboBox, s3Num),
                    new SubjectGradeInput(s4ComboBox, s4Num),
                    new SubjectGradeInput(s5ComboBox, s5Num),
                    new SubjectGradeInput(s6ComboBox, s6Num),
                    new SubjectGradeInput(s7ComboBox, s7Num),
                    new SubjectGradeInput(s8ComboBox, s8Num),
                ];

            // Allow the user to select any subject from the database
            foreach (SubjectGradeInput input in _subjectGradeInputs)
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
            foreach (SubjectGradeInput inputs in _subjectGradeInputs)
            {
                if (inputs.SubjectInput.SelectedItem == null)
                {
                    continue;
                }

                _learner.addSubjectMark((Subject)inputs.SubjectInput.SelectedItem, (int)inputs.GradeInput.Value);
            }

            Close();
        }
    }
}
