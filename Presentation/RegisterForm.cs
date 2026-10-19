using FuturePathGroup10.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FuturePathGroup10.Presentation
{
    public partial class RegisterForm : Form
    {
        private readonly UserAccountDB _userAccountDb;

        public RegisterForm(UserAccountDB userAccountDb)
        {
            InitializeComponent();
            _userAccountDb = userAccountDb;
        }

        private void createAccountButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == string.Empty
                || gradeComboBox.SelectedItem is null
                || schoolTextBox.Text == string.Empty
                || provinceComboBox.SelectedItem is null
                || phoneTextBox.Text == string.Empty
                || emailTextBox.Text == string.Empty
                || passwordTextBox.Text == string.Empty
                || rePasswordTextBox.Text == string.Empty
                )
            {
                MessageBox.Show(
                    "All registration fields must be filled out.",
                    "Missing Fields",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            // Regex for a valid phone number
            if (!Regex.IsMatch(phoneTextBox.Text, "^\\+?[\\d\\s\\-()]+$"))
            {
                MessageBox.Show(
                    "Please enter a valid phone number.",
                    "Invalid Phone Number",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            try
            {
                MailAddress email = new(emailTextBox.Text);
            }
            catch
            {
                MessageBox.Show(
                    "Please enter a valid email address.",
                    "Invalid Email",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            if (passwordTextBox.Text != rePasswordTextBox.Text)
            {
                MessageBox.Show(
                    "Please ensure both passwords are the same.",
                    "Invalid Password",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            FuturePathMDIParent parent = (FuturePathMDIParent)MdiParent;
            parent.Register(
                nameTextBox.Text, 
                int.TryParse(gradeComboBox.SelectedItem), 
                schoolTextBox.Text, 
                (string)provinceComboBox.SelectedItem, 
                phoneTextBox.Text, 
                emailTextBox.SelectedText, 
                passwordTextBox.Text
            );
        }
    }
}
