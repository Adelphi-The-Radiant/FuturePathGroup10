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
            throw new NotImplementedException();
        }
    }
}
