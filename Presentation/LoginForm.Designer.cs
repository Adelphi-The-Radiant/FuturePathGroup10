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
            whiteBg.Location = new Point(268, 117);
            whiteBg.Name = "whiteBg";
            whiteBg.Size = new Size(306, 222);
            whiteBg.TabIndex = 0;
            whiteBg.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(66, 159, 132);
            pictureBox1.Location = new Point(268, 89);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(306, 31);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // loginButton
            // 
            loginButton.BackColor = Color.FromArgb(66, 159, 132);
            loginButton.Cursor = Cursors.Hand;
            loginButton.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            loginButton.ForeColor = Color.White;
            loginButton.Location = new Point(302, 252);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(250, 39);
            loginButton.TabIndex = 2;
            loginButton.Text = "Login";
            loginButton.UseVisualStyleBackColor = false;
            // 
            // noAccountLabel
            // 
            noAccountLabel.AutoSize = true;
            noAccountLabel.BackColor = Color.White;
            noAccountLabel.Font = new Font("Arial Narrow", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            noAccountLabel.Location = new Point(302, 304);
            noAccountLabel.Name = "noAccountLabel";
            noAccountLabel.Size = new Size(118, 16);
            noAccountLabel.TabIndex = 3;
            noAccountLabel.Text = "Don't have an account?";
            // 
            // registerLinkLabel
            // 
            registerLinkLabel.AutoSize = true;
            registerLinkLabel.BackColor = Color.White;
            registerLinkLabel.DisabledLinkColor = Color.FromArgb(66, 159, 132);
            registerLinkLabel.Font = new Font("Arial Narrow", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            registerLinkLabel.LinkColor = Color.FromArgb(66, 159, 132);
            registerLinkLabel.Location = new Point(426, 304);
            registerLinkLabel.Name = "registerLinkLabel";
            registerLinkLabel.Size = new Size(71, 16);
            registerLinkLabel.TabIndex = 4;
            registerLinkLabel.TabStop = true;
            registerLinkLabel.Text = "Register here";
            // 
            // nameTextBox
            // 
            nameTextBox.BorderStyle = BorderStyle.FixedSingle;
            nameTextBox.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nameTextBox.Location = new Point(341, 139);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(212, 25);
            nameTextBox.TabIndex = 5;
            nameTextBox.Text = "Enter Name";
            nameTextBox.TextChanged += nameTextBox_TextChanged;
            // 
            // passwordTextBox
            // 
            passwordTextBox.BorderStyle = BorderStyle.FixedSingle;
            passwordTextBox.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            passwordTextBox.Location = new Point(341, 181);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(212, 25);
            passwordTextBox.TabIndex = 6;
            passwordTextBox.Text = "Enter Password";
            // 
            // loginLabel
            // 
            loginLabel.AutoSize = true;
            loginLabel.BackColor = Color.FromArgb(66, 159, 132);
            loginLabel.Font = new Font("Arial Narrow", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            loginLabel.ForeColor = Color.White;
            loginLabel.Location = new Point(386, 89);
            loginLabel.Name = "loginLabel";
            loginLabel.Size = new Size(67, 31);
            loginLabel.TabIndex = 7;
            loginLabel.Text = "Login";
            // 
            // forgotPasswordLinkLabel
            // 
            forgotPasswordLinkLabel.AutoSize = true;
            forgotPasswordLinkLabel.BackColor = Color.White;
            forgotPasswordLinkLabel.LinkColor = Color.FromArgb(66, 159, 132);
            forgotPasswordLinkLabel.Location = new Point(305, 219);
            forgotPasswordLinkLabel.Name = "forgotPasswordLinkLabel";
            forgotPasswordLinkLabel.Size = new Size(100, 15);
            forgotPasswordLinkLabel.TabIndex = 8;
            forgotPasswordLinkLabel.TabStop = true;
            forgotPasswordLinkLabel.Text = "Forgot Password?";
            // 
            // iconPictureBox
            // 
            iconPictureBox.BackColor = Color.White;
            iconPictureBox.Image = Properties.Resources.Screenshot_2026_10_14_112451;
            iconPictureBox.Location = new Point(301, 139);
            iconPictureBox.Name = "iconPictureBox";
            iconPictureBox.Size = new Size(34, 25);
            iconPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            iconPictureBox.TabIndex = 9;
            iconPictureBox.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.White;
            pictureBox2.Image = Properties.Resources.Screenshot_2026_10_14_112519;
            pictureBox2.Location = new Point(301, 181);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(34, 25);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 10;
            pictureBox2.TabStop = false;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(79, 184, 156);
            ClientSize = new Size(800, 450);
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