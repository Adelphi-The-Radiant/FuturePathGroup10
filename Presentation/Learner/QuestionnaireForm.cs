using FuturePathGroup10.Learner;
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

        public QuestionnaireForm(LearnerController learnerController)
        {
            InitializeComponent();
            _learnerController = learnerController;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;
            parent.OpenMarksCapture();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            List<NumericUpDown> inputs = [
                q1Num,
                q2Num,
                q3Num,
                q4Num,
                q5Num,
                q6Num,
                q7Num,
                q8Num
             ];

            foreach (NumericUpDown input in inputs)
            {
                _learnerController.saveAnswer(0, (int)q1Num.Value, out string error);

                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show(error);
                    return;
                }
            }
        }
    }
}
