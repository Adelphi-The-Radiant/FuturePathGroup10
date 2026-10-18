using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FuturePathGroup10.Data;
using System.Linq;

namespace FuturePathGroup10.Presentation
{
    public partial class LoginForm : Form
    {
        private readonly UserAccountDB _userAccountDb;

        public LoginForm(UserAccountDB userAccountDb)
        {
            InitializeComponent();
            _userAccountDb = userAccountDb;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == string.Empty || passwordTextBox.Text == string.Empty)
            {
                MessageBox.Show(
                    "Username and password must be filled in to log in.",
                    "Incomplete Login Details",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            UserAccount loginAccount;
            try
            {
                loginAccount = _userAccountDb.accounts.First(account => account.username == nameTextBox.Text);
            }
            catch
            {
                MessageBox.Show(
                    "Username does not exist. Please register if you want to create an account.",
                    "Username Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            DialogResult proceedResult = MessageBox.Show(
                "Login successful. Your home page will now open.",
                "Login Success",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information
            );
            if (proceedResult != DialogResult.OK)
            {
                return;
            }

            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;
            parent.Login(loginAccount);
        }

        private void registerLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;
            parent.StartRegistration();
        }
    }
}
