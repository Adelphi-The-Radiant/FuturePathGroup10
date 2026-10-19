using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            _learnerController.loadLearner(authorizedAccount.username);

            CurrentForm = new HomeForm();
        }

        public void Logout()
        {
            _userAccount = null;
            _learnerController.loadLearner(null);
            CurrentForm = new LoginForm(_userAccountDb);
        }

        public void StartRegistration()
        {
            CurrentForm = new RegisterForm(_userAccountDb);
        }

        public void Register(string username, int grade, string school, string province, string phone, string email, string password)
        {
            username = username.Trim();

            if (_userAccountDb.accounts.Any(a => string.Equals(a.username?.Trim(), username, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show(
                    "That username is already taken. Please choose another one.",
                    "Username Taken",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            FuturePathGroup10.Learner.Learner? existingLearner = _learnerController.learnerDB.learners.FirstOrDefault(
                l => string.Equals(l.Username?.Trim(), username, StringComparison.OrdinalIgnoreCase)
            );

            if (existingLearner is null && _learnerController.learnerDB.learners.Any(
                l => string.Equals(l.email?.Trim(), email.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show(
                    "That email address is already registered. Please use another one.",
                    "Email Taken",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            UserAccount newAccount = new(username, password, DateTime.Now, UserAccount.Permission.user);
            try
            {
                _userAccountDb.DataSetChange(newAccount, DB.DBOperation.Add);
                if (!_userAccountDb.UpdateDataSource(DB.DBOperation.Add))
                {
                    _userAccountDb.accounts.Remove(newAccount);
                    MessageBox.Show("Could not save your account. Please try again.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                _userAccountDb.accounts.Remove(newAccount);
                MessageBox.Show("Could not save your account: " + ex.Message, "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (existingLearner is not null)
            {
                existingLearner.Account = newAccount;
                _learnerController.loadLearner(username);
            }
            else
            {
                FuturePathGroup10.Learner.Learner newLearner = new(0, username, phone.Trim(), username, school.Trim(), null, null, email.Trim(), province);
                newLearner.Account = newAccount;

                if (!_learnerController.createProfile(newLearner, out string error))
                {
                    try
                    {
                        _userAccountDb.DataSetChange(newAccount, DB.DBOperation.Delete);
                        _userAccountDb.UpdateDataSource(DB.DBOperation.Delete);
                    }
                    catch (Exception) { }

                    MessageBox.Show(error, "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            MessageBox.Show(
                "Your account has been created. Your home page will now open.",
                "Registration Successful",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            _userAccount = newAccount;
            CurrentForm = new HomeForm();
        }

        public void StartInterestQuestionnaire()
        {
            if (!_learnerController.startQuestionnaire(out string error))
            {
                MessageBox.Show(
                    error,
                    "Could Not Start Questionnaire",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            CurrentForm = new Learner.QuestionnaireForm(_learnerController);
        }

        public void OpenMarksCapture()
        {
            CurrentForm = new Learner.CaptureMarksForm(_learnerController.subjectDB, _learnerController);
        }

        public void OpenQualificationScreen()
        {
            CurrentForm = new Learner.SelectQualificationForm(_learnerController);
        }

        public void OpenAlternatives()
        {
            CurrentForm = new Learner.AlternativesForm(_learnerController);
        }

        public void OpenFunding(FuturePathGroup10.Reccomendation.Degree degree, bool fromAlternatives)
        {
            try
            {
                _learnerController.viewFundingOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not load the funding options: " + ex.Message,
                    "Funding Options",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            CurrentForm = new Learner.FundingForm(degree.options.FirstOrDefault(), degree, fromAlternatives);
        }

        public void OpenLearnerReport()
        {
            CurrentForm = new Learner.LearnerReportForm(_learnerController);
        }

        public void StartAdvisoryForm()
        {
            CurrentForm = new Learner.EligibilityForm(_learnerController);
        }

        private void CurrentForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            ReturnHome();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logout();
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

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
