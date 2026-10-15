using FuturePathGroup10.Business;
using FuturePathGroup10.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace FuturePathGroup10.Presentation
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            Learner learner = new Learner(
                2,
                "thabo.mokoena",
                "0821234567",
                "thabo@example.com",
                "Thabo Mokoena",
                "Chris Hani Secondary School",
                "80",
                "78",
                "Western Cape"
            );

            LearnerController controller = new LearnerController();
            controller.createProfile(learner, out string erro);
            MessageBox.Show("Saved. Learner ID: " + learner.learnerID);


        }
    }
}
