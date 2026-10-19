using FuturePathGroup10.Data;
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
    public partial class SelectQualificationForm : Form
    {
        private readonly LearnerController _learnerController;

        public SelectQualificationForm(LearnerController learnerController)
        {
            InitializeComponent();
            _learnerController = learnerController;
            facultyComboBox.DataSource = _learnerController.facultyDB.faculties;
            qualificationComboBox.DataSource = _learnerController.degreeDB.degrees;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;
            parent.ReturnHome();
        }

        private void beginButton_Click(object sender, EventArgs e)
        {
            if (facultyComboBox.SelectedItem is null && qualificationComboBox.SelectedItem is null)
            {
                MessageBox.Show(
                    "At least one of Faculty or Qualification must be selected.",
                    "Must Select Option",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            if (facultyComboBox.SelectedItem is not null)
            {
                _learnerController.selectFaculty((Reccomendation.Faculty)facultyComboBox.SelectedItem, out string error);
            }

            if (qualificationComboBox.SelectedItem is not null)
            {
                _learnerController.selectDegree((Reccomendation.Degree)qualificationComboBox.SelectedItem, out string error);
            }

            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;
            parent.StartAdvisoryForm();
        }
    }
}
