namespace FuturePathGroup10.Presentation
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            whiteBg = new PictureBox();
            pictureBox1 = new PictureBox();
            loginButton = new Button();
            noAccountLabel = new Label();
            registerLinkLabel = new LinkLabel();
            nameTextBox = new TextBox();
            passwordTextBox = new TextBox();
            loginLabel = new Label();
            forgotPasswordLinkLabel = new LinkLabel();
            iconPictureBox = new PictureBox();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)whiteBg).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // whiteBg
            // 
            whiteBg.BackColor = Color.White;
            whiteBg.Location = new Point(306, 156);
            whiteBg.Margin = new Padding(3, 4, 3, 4);
            whiteBg.Name = "whiteBg";
            whiteBg.Size = new Size(350, 296);
            whiteBg.TabIndex = 0;
            whiteBg.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(66, 159, 132);
            pictureBox1.Location = new Point(306, 119);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(350, 41);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // loginButton
            // 
            loginButton.BackColor = Color.FromArgb(66, 159, 132);
            loginButton.Cursor = Cursors.Hand;
            loginButton.FlatStyle = FlatStyle.Popup;
            loginButton.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            loginButton.ForeColor = Color.White;
            loginButton.Location = new Point(345, 336);
            loginButton.Margin = new Padding(3, 4, 3, 4);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(286, 52);
            loginButton.TabIndex = 2;
            loginButton.Text = "Login";
            loginButton.UseVisualStyleBackColor = false;
            loginButton.Click += loginButton_Click;
            // 
            // noAccountLabel
            // 
            noAccountLabel.AutoSize = true;
            noAccountLabel.BackColor = Color.White;
            noAccountLabel.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            noAccountLabel.Location = new Point(345, 405);
            noAccountLabel.Name = "noAccountLabel";
            noAccountLabel.Size = new Size(185, 20);
            noAccountLabel.TabIndex = 3;
            noAccountLabel.Text = "Don't have an account?";
            // 
            // registerLinkLabel
            // 
            registerLinkLabel.AutoSize = true;
            registerLinkLabel.BackColor = Color.White;
            registerLinkLabel.DisabledLinkColor = Color.FromArgb(66, 159, 132);
            registerLinkLabel.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            registerLinkLabel.LinkColor = Color.FromArgb(66, 159, 132);
            registerLinkLabel.Location = new Point(487, 405);
            registerLinkLabel.Name = "registerLinkLabel";
            registerLinkLabel.Size = new Size(110, 20);
            registerLinkLabel.TabIndex = 4;
            registerLinkLabel.TabStop = true;
            registerLinkLabel.Text = "Register here";
            registerLinkLabel.LinkClicked += registerLinkLabel_LinkClicked;
            // 
            // nameTextBox
            // 
            nameTextBox.BorderStyle = BorderStyle.FixedSingle;
            nameTextBox.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nameTextBox.Location = new Point(390, 185);
            nameTextBox.Margin = new Padding(3, 4, 3, 4);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.PlaceholderText = "Enter Name";
            nameTextBox.Size = new Size(242, 29);
            nameTextBox.TabIndex = 5;
            // 
            // passwordTextBox
            // 
            passwordTextBox.BorderStyle = BorderStyle.FixedSingle;
            passwordTextBox.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            passwordTextBox.Location = new Point(390, 241);
            passwordTextBox.Margin = new Padding(3, 4, 3, 4);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PlaceholderText = "Enter Password";
            passwordTextBox.Size = new Size(242, 29);
            passwordTextBox.TabIndex = 6;
            passwordTextBox.UseSystemPasswordChar = true;
            // 
            // loginLabel
            // 
            loginLabel.AutoSize = true;
            loginLabel.BackColor = Color.FromArgb(66, 159, 132);
            loginLabel.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            loginLabel.ForeColor = Color.White;
            loginLabel.Location = new Point(441, 119);
            loginLabel.Name = "loginLabel";
            loginLabel.Size = new Size(101, 39);
            loginLabel.TabIndex = 7;
            loginLabel.Text = "Login";
            // 
            // forgotPasswordLinkLabel
            // 
            forgotPasswordLinkLabel.AutoSize = true;
            forgotPasswordLinkLabel.BackColor = Color.White;
            forgotPasswordLinkLabel.LinkColor = Color.FromArgb(66, 159, 132);
            forgotPasswordLinkLabel.Location = new Point(349, 292);
            forgotPasswordLinkLabel.Name = "forgotPasswordLinkLabel";
            forgotPasswordLinkLabel.Size = new Size(125, 20);
            forgotPasswordLinkLabel.TabIndex = 8;
            forgotPasswordLinkLabel.TabStop = true;
            forgotPasswordLinkLabel.Text = "Forgot Password?";
            // 
            // iconPictureBox
            // 
            iconPictureBox.BackColor = Color.White;
            iconPictureBox.Image = Properties.Resources.Screenshot_2026_10_14_112451;
            iconPictureBox.Location = new Point(344, 185);
            iconPictureBox.Margin = new Padding(3, 4, 3, 4);
            iconPictureBox.Name = "iconPictureBox";
            iconPictureBox.Size = new Size(39, 33);
            iconPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            iconPictureBox.TabIndex = 9;
            iconPictureBox.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.White;
            pictureBox2.Image = Properties.Resources.Screenshot_2026_10_14_112519;
            pictureBox2.Location = new Point(344, 241);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(39, 33);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 10;
            pictureBox2.TabStop = false;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(79, 184, 156);
            ClientSize = new Size(914, 600);
            Controls.Add(pictureBox2);
            Controls.Add(iconPictureBox);
            Controls.Add(forgotPasswordLinkLabel);
            Controls.Add(loginLabel);
            Controls.Add(passwordTextBox);
            Controls.Add(nameTextBox);
            Controls.Add(registerLinkLabel);
            Controls.Add(noAccountLabel);
            Controls.Add(loginButton);
            Controls.Add(pictureBox1);
            Controls.Add(whiteBg);
            Margin = new Padding(3, 4, 3, 4);
            Name = "LoginForm";
            Text = "LoginForm";
            ((System.ComponentModel.ISupportInitialize)whiteBg).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox whiteBg;
        private PictureBox pictureBox1;
        private Button loginButton;
        private Label noAccountLabel;
        private LinkLabel registerLinkLabel;
        private TextBox nameTextBox;
        private TextBox passwordTextBox;
        private Label loginLabel;
        private LinkLabel forgotPasswordLinkLabel;
        private PictureBox iconPictureBox;
        private PictureBox pictureBox2;
    }
}