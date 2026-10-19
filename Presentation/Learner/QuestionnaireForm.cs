using FuturePathGroup10.Learner;
using System.Linq;
using FuturePathGroup10.Interest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FuturePathGroup10.Presentation.Learner
{
    public partial class QuestionnaireForm : Form
    {
        private readonly LearnerController _learnerController;
        private readonly List<NumericUpDown> _inputs;
        private readonly List<InterestQuestion> _questions = [];
        private readonly HashSet<int> _savedQuestionIDs = [];

        public QuestionnaireForm(LearnerController learnerController)
        {
            InitializeComponent();
            _learnerController = learnerController;

            _inputs = [q1Num, q2Num, q3Num, q4Num, q5Num, q6Num, q7Num, q8Num];

            try
            {
                _questions = _learnerController.getQuestions().OrderBy(q => q.questionID).Take(_inputs.Count).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load the questionnaire questions: " + ex.Message);
            }

            for (int i = _questions.Count; i < _inputs.Count; i++)
            {
                _inputs[i].Enabled = false;
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;
            parent.OpenMarksCapture();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (_questions.Count == 0)
            {
                MessageBox.Show(
                    "There are no questionnaire questions in the database.",
                    "No Questions",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            for (int i = 0; i < _questions.Count; i++)
            {
                if (_inputs[i].Value < 1)
                {
                    MessageBox.Show(
                        "Please rate every question from 1 to 5.",
                        "Incomplete Questionnaire",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }
            }

            for (int i = 0; i < _questions.Count; i++)
            {
                int questionID = _questions[i].questionID;
                if (_savedQuestionIDs.Contains(questionID)) continue;

                if (!_learnerController.saveAnswer(questionID, (int)_inputs[i].Value, out string error))
                {
                    MessageBox.Show(error, "Could Not Save Answer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _savedQuestionIDs.Add(questionID);
            }

            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;
            parent.OpenQualificationScreen();
        }
    }
}
