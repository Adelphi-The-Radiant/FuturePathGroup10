using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FuturePathGroup10.Data;
using FuturePathGroup10.Learner;

namespace FuturePathGroup10.Presentation
{
    public partial class FuturePathMDIParent : Form
    {
        private readonly AdminDB _adminDb;
        private readonly CareerAdvisorDB _careerAdvisorDb;
        private readonly UserAccountDB _userAccountDb;
        private readonly LearnerController _learnerController;
        private UserAccount? _userAccount;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Form CurrentForm
        {
            get;
            set
            {
                if (field == value) return;

                Form oldForm = field;
                field = value;

                field.FormClosed += CurrentForm_FormClosed;
                field.MdiParent = this;
                field.Show();

                if (oldForm is not null)
                {
                    oldForm.FormClosed -= CurrentForm_FormClosed;
                    oldForm.Close();
                }
            }
        }

        public FuturePathMDIParent()
        {
            InitializeComponent();
            IsMdiContainer = true;

            _learnerController = new();

            // Initialize the databases
            _adminDb = new();
            _careerAdvisorDb = new();
            _userAccountDb = new(_learnerController.learnerDB, _adminDb, _careerAdvisorDb);

            // Open the startup window
            CurrentForm = new LoginForm(_userAccountDb);
        }

        public void ReturnHome() 
        {
            if (_userAccount is null)
            {
                CurrentForm = new LoginForm(_userAccountDb);
                return;
            }

            CurrentForm = new HomeForm();
        }

        public void Login(UserAccount authorizedAccount)
        {
            if (_userAccount is not null)
            {
                throw new ArgumentException("Cannot log into an account while already logged in.");
            }

            _userAccount = authorizedAccount;

            CurrentForm = new HomeForm();
        }

        public void StartRegistration()
        {
            CurrentForm = new RegisterForm(_userAccountDb);
        }

        public void Register(string username, int grade, string school, string province, string phone, string email, string password)
        {
            UserAccount newAccount = new(username, password, DateTime.Now, UserAccount.Permission.user);
            FuturePathGroup10.Learner.Learner newLearner = new(_learnerController.learnerDB.getNextID(), username, phone, username, school, null, null, email, province);
            _learnerController.createProfile(newLearner, out string error);
            CurrentForm = new HomeForm();
        }

        public void StartInterestQuestionnaire()
        {
            CurrentForm = new Learner.QuestionnaireForm(_learnerController);
        }
        public void OpenMarksCapture()
        {
            CurrentForm = new Learner.CaptureMarksForm(_learnerController.subjectDB, _learnerController);
        }

        private void CurrentForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            ReturnHome();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentForm = new LoginForm(_userAccountDb);
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
    }
}
