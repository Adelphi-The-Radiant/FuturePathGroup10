using FuturePathGroup10.Business;
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
    public partial class AlternativesForm : Form
    {
        DegreeDB degreeDB;
        Learner.Learner learner;
        public AlternativesForm(DegreeDB degreeDB, Learner.Learner learner)
        {
            InitializeComponent();
            this.degreeDB = degreeDB;
            this.learner = learner;

            display();
        }

        private void display()
        {
            string output = "";
            foreach (var deg in degreeDB.degrees)
            {
                Collection<ProgrammeRequirement> p = deg.requirements;
                foreach (var req in p)
                {
                    if (req.subject in learner.LearnerSubject) {
                        LearnerSubject ls = learner.LearnerSubject;
                        if (ls.result >=req.minRequiredGrade)
                        {
                            output += deg.GetDegree();
                        }
                    }
                }
            }
            listLabel.Text = output;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            HomeForm hf = new HomeForm();
            hf.Show();
            this.Close();
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            LearnerReportForm rf = new LearnerReportForm();
            rf.Show();
            this.Close();
        }

        private void fundButton_Click(object sender, EventArgs e)
        {
            FundingForm ff = new FundingForm();
            ff.Show();
            this.Close();
        }
    }
}
