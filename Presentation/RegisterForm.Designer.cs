namespace FuturePathGroup10.Presentation
{
    partial class RegisterForm
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
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            createAccountButton = new Button();
            nameTextBox = new TextBox();
            passwordTextBox = new TextBox();
            rePasswordTextBox = new TextBox();
            emailTextBox = new TextBox();
            gradeComboBox = new ComboBox();
            provinceComboBox = new ComboBox();
            phoneTextBox = new TextBox();
            label1 = new Label();
            schoolTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Location = new Point(250, 95);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(405, 463);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(66, 159, 132);
            pictureBox2.Location = new Point(250, 41);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(405, 57);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // createAccountButton
            // 
            createAccountButton.BackColor = Color.FromArgb(66, 159, 132);
            createAccountButton.Cursor = Cursors.Hand;
            createAccountButton.FlatStyle = FlatStyle.Popup;
            createAccountButton.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            createAccountButton.ForeColor = Color.White;
            createAccountButton.Location = new Point(291, 481);
            createAccountButton.Margin = new Padding(3, 4, 3, 4);
            createAccountButton.Name = "createAccountButton";
            createAccountButton.Size = new Size(322, 56);
            createAccountButton.TabIndex = 2;
            createAccountButton.Text = "Create Account";
            createAccountButton.UseVisualStyleBackColor = false;
            createAccountButton.Click += createAccountButton_Click;
            // 
            // nameTextBox
            // 
            nameTextBox.BorderStyle = BorderStyle.FixedSingle;
            nameTextBox.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nameTextBox.Location = new Point(291, 132);
            nameTextBox.Margin = new Padding(3, 4, 3, 4);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.PlaceholderText = "Enter Name";
            nameTextBox.Size = new Size(322, 29);
            nameTextBox.TabIndex = 3;
            // 
            // passwordTextBox
            // 
            passwordTextBox.BorderStyle = BorderStyle.FixedSingle;
            passwordTextBox.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            passwordTextBox.Location = new Point(291, 392);
            passwordTextBox.Margin = new Padding(3, 4, 3, 4);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PlaceholderText = "Enter Password";
            passwordTextBox.Size = new Size(321, 29);
            passwordTextBox.TabIndex = 4;
            passwordTextBox.UseSystemPasswordChar = true;
            // 
            // rePasswordTextBox
            // 
            rePasswordTextBox.BorderStyle = BorderStyle.FixedSingle;
            rePasswordTextBox.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rePasswordTextBox.Location = new Point(290, 433);
            rePasswordTextBox.Margin = new Padding(3, 4, 3, 4);
            rePasswordTextBox.Name = "rePasswordTextBox";
            rePasswordTextBox.PlaceholderText = "Reenter Password ";
            rePasswordTextBox.Size = new Size(322, 29);
            rePasswordTextBox.TabIndex = 5;
            rePasswordTextBox.UseSystemPasswordChar = true;
            // 
            // emailTextBox
            // 
            emailTextBox.BorderStyle = BorderStyle.FixedSingle;
            emailTextBox.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            emailTextBox.Location = new Point(290, 351);
            emailTextBox.Margin = new Padding(3, 4, 3, 4);
            emailTextBox.Name = "emailTextBox";
            emailTextBox.PlaceholderText = "Enter Email";
            emailTextBox.Size = new Size(322, 29);
            emailTextBox.TabIndex = 6;
            // 
            // gradeComboBox
            // 
            gradeComboBox.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gradeComboBox.FormattingEnabled = true;
            gradeComboBox.Items.AddRange(new object[] { "11", "12" });
            gradeComboBox.Location = new Point(291, 173);
            gradeComboBox.Margin = new Padding(3, 4, 3, 4);
            gradeComboBox.Name = "gradeComboBox";
            gradeComboBox.Size = new Size(322, 32);
            gradeComboBox.TabIndex = 7;
            gradeComboBox.Text = "Select Grade";
            // 
            // provinceComboBox
            // 
            provinceComboBox.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            provinceComboBox.FormattingEnabled = true;
            provinceComboBox.Items.AddRange(new object[] { "Gauteng", "Western Cape", "Eastern Cape", "Northern Cape", "Free State", "Limpopo", "North West", "Kwazulu-Natal", "Mpumalanga" });
            provinceComboBox.Location = new Point(291, 264);
            provinceComboBox.Margin = new Padding(3, 4, 3, 4);
            provinceComboBox.Name = "provinceComboBox";
            provinceComboBox.Size = new Size(321, 32);
            provinceComboBox.TabIndex = 9;
            provinceComboBox.Text = "Select Province";
            // 
            // phoneTextBox
            // 
            phoneTextBox.BorderStyle = BorderStyle.FixedSingle;
            phoneTextBox.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            phoneTextBox.Location = new Point(290, 309);
            phoneTextBox.Margin = new Padding(3, 4, 3, 4);
            phoneTextBox.Name = "phoneTextBox";
            phoneTextBox.PlaceholderText = "Enter Phone Number";
            phoneTextBox.Size = new Size(322, 29);
            phoneTextBox.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(66, 159, 132);
            label1.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(386, 47);
            label1.Name = "label1";
            label1.Size = new Size(168, 46);
            label1.TabIndex = 11;
            label1.Text = "Register";
            // 
            // schoolTextBox
            // 
            schoolTextBox.BorderStyle = BorderStyle.FixedSingle;
            schoolTextBox.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            schoolTextBox.Location = new Point(291, 219);
            schoolTextBox.Margin = new Padding(3, 4, 3, 4);
            schoolTextBox.Name = "schoolTextBox";
            schoolTextBox.PlaceholderText = "Enter School";
            schoolTextBox.Size = new Size(322, 29);
            schoolTextBox.TabIndex = 12;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(79, 184, 156);
            ClientSize = new Size(914, 600);
            Controls.Add(schoolTextBox);
            Controls.Add(label1);
            Controls.Add(phoneTextBox);
            Controls.Add(provinceComboBox);
            Controls.Add(gradeComboBox);
            Controls.Add(emailTextBox);
            Controls.Add(rePasswordTextBox);
            Controls.Add(passwordTextBox);
            Controls.Add(nameTextBox);
            Controls.Add(createAccountButton);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "RegisterForm";
            Text = "RegisterForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Button createAccountButton;
        private TextBox nameTextBox;
        private TextBox passwordTextBox;
        private TextBox rePasswordTextBox;
        private TextBox emailTextBox;
        private ComboBox gradeComboBox;
        private ComboBox provinceComboBox;
        private TextBox phoneTextBox;
        private Label label1;
        private TextBox schoolTextBox;
    }
}