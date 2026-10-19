using FuturePathGroup10.Administration;
using FuturePathGroup10.Learner;
using FuturePathGroup10.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FuturePathGroup10.Presentation.Advisory
{
    public partial class GenerateReportForm : Form
    {
        public Administrator admin;
        AdministratorController administratorController;
        //not sure where the created report goes but can be 
        private Dictionary<string, string> learnerReports = new Dictionary<string, string>();

        public Learner.learner selectedLearner;
        public GenerateReportForm()
        {
            InitializeComponent();
            LearnerDB learnerDB = new LearnerDB();
            FacultyDB facultyDB = new FacultyDB(learnerDB);
            DegreeDB degreeDB = new DegreeDB(facultyDB);

            this.administratorController = new AdministratorController(this.admin, degreeDB, facultyDB);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedLearner =
                comboBox1.SelectedItem as Learner.Learner;

            if (selectedLearner != null &&
                learnerReports.TryGetValue(selectedLearner.fullname, out string report))
            {
                reportTextBox.Text = report;
            }
            else
            {
                reportTextBox.Text = "";
            }
        }

        private void GenerateReportForm_Load(object sender, EventArgs e)
        {
            Collection<Learner.Learner> learners =
                administratorController.generateComboBox();

            comboBox1.DataSource = learners;
            comboBox1.DisplayMember = "fullname";
        }

        private void reportTextBox_TextChanged(object sender, EventArgs e)
        {
            if (selectedLearner != null)
            {
                learnerReports[selectedLearner.fullname] = reportTextBox.Text;
            }
        }
    }
}
