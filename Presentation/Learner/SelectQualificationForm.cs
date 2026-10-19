using Faculty = FuturePathGroup10.Reccomendation.Faculty;
using Degree = FuturePathGroup10.Reccomendation.Degree;
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

        private readonly Faculty _anyFaculty = new(0, "(Any faculty)", "", null!);
        private readonly Degree _anyDegree = new(0, "(All qualifications)", "");

        public SelectQualificationForm(LearnerController learnerController)
        {
            InitializeComponent();
            _learnerController = learnerController;

            List<Faculty> faculties = [_anyFaculty];
            faculties.AddRange(_learnerController.facultyDB.faculties);
            facultyComboBox.DataSource = faculties;
            facultyComboBox.SelectedIndex = 0;

            ShowQualifications(null);
        }

        private void ShowQualifications(Faculty? faculty)
        {
            Degree? previous = qualificationComboBox.SelectedItem as Degree;

            List<Degree> options = [_anyDegree];
            options.AddRange(faculty is null ? _learnerController.degreeDB.degrees : faculty.degreesOffered);

            qualificationComboBox.DataSource = options;

            qualificationComboBox.SelectedItem = previous is not null && options.Contains(previous) ? previous : _anyDegree;
        }

        private void facultyComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Faculty? chosen = facultyComboBox.SelectedItem as Faculty;
            ShowQualifications(chosen == _anyFaculty ? null : chosen);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;
            parent.ReturnHome();
        }

        private void beginButton_Click(object sender, EventArgs e)
        {
            Faculty? faculty = facultyComboBox.SelectedItem as Faculty;
            if (faculty == _anyFaculty) faculty = null;

            Degree? degree = qualificationComboBox.SelectedItem as Degree;
            if (degree == _anyDegree) degree = null;

            if (faculty is null && degree is null)
            {
                MessageBox.Show(
                    "At least one of Faculty or Qualification must be selected.",
                    "Must Select Option",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            _learnerController.clearSelection();

            if (faculty is not null)
            {
                _learnerController.selectFaculty(faculty, out string error);
            }

            if (degree is not null)
            {
                _learnerController.selectDegree(degree, out string error);
            }

            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;
            parent.StartAdvisoryForm();
        }
    }
}
