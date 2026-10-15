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
            button1 = new Button();
            nameTextBox = new TextBox();
            passwordTextBox = new TextBox();
            rePasswordTextBox = new TextBox();
            emailTextBox = new TextBox();
            gradeComboBox = new ComboBox();
            schoolComboBox = new ComboBox();
            provinceComboBox = new ComboBox();
            phoneTextBox = new TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Location = new Point(219, 71);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(354, 347);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(66, 159, 132);
            pictureBox2.Location = new Point(219, 31);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(354, 43);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(66, 159, 132);
            button1.Cursor = Cursors.Hand;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Arial Narrow", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(255, 361);
            button1.Name = "button1";
            button1.Size = new Size(282, 42);
            button1.TabIndex = 2;
            button1.Text = "Create Account";
            button1.UseVisualStyleBackColor = false;
            // 
            // nameTextBox
            // 
            nameTextBox.BorderStyle = BorderStyle.FixedSingle;
            nameTextBox.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nameTextBox.Location = new Point(255, 99);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(282, 25);
            nameTextBox.TabIndex = 3;
            nameTextBox.Text = "Enter Name";
            // 
            // passwordTextBox
            // 
            passwordTextBox.BorderStyle = BorderStyle.FixedSingle;
            passwordTextBox.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            passwordTextBox.Location = new Point(255, 294);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(281, 25);
            passwordTextBox.TabIndex = 4;
            passwordTextBox.Text = "Enter Password";
            // 
            // rePasswordTextBox
            // 
            rePasswordTextBox.BorderStyle = BorderStyle.FixedSingle;
            rePasswordTextBox.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rePasswordTextBox.Location = new Point(254, 325);
            rePasswordTextBox.Name = "rePasswordTextBox";
            rePasswordTextBox.Size = new Size(282, 25);
            rePasswordTextBox.TabIndex = 5;
            rePasswordTextBox.Text = "Reenter Password ";
            // 
            // emailTextBox
            // 
            emailTextBox.BorderStyle = BorderStyle.FixedSingle;
            emailTextBox.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            emailTextBox.Location = new Point(254, 263);
            emailTextBox.Name = "emailTextBox";
            emailTextBox.Size = new Size(282, 25);
            emailTextBox.TabIndex = 6;
            emailTextBox.Text = "Enter Email";
            // 
            // gradeComboBox
            // 
            gradeComboBox.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gradeComboBox.FormattingEnabled = true;
            gradeComboBox.Items.AddRange(new object[] { "11", "12" });
            gradeComboBox.Location = new Point(255, 130);
            gradeComboBox.Name = "gradeComboBox";
            gradeComboBox.Size = new Size(282, 28);
            gradeComboBox.TabIndex = 7;
            gradeComboBox.Text = "Select Grade";
            // 
            // schoolComboBox
            // 
            schoolComboBox.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            schoolComboBox.FormattingEnabled = true;
            schoolComboBox.Location = new Point(255, 164);
            schoolComboBox.Name = "schoolComboBox";
            schoolComboBox.Size = new Size(281, 28);
            schoolComboBox.TabIndex = 8;
            schoolComboBox.Text = "Select School";
            // 
            // provinceComboBox
            // 
            provinceComboBox.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            provinceComboBox.FormattingEnabled = true;
            provinceComboBox.Items.AddRange(new object[] { "Gauteng", "Western Cape", "Eastern Cape", "Northern Cape", "Free State", "Limpopo", "North West", "Kwazulu-Natal", "Mpumalanga" });
            provinceComboBox.Location = new Point(255, 198);
            provinceComboBox.Name = "provinceComboBox";
            provinceComboBox.Size = new Size(281, 28);
            provinceComboBox.TabIndex = 9;
            provinceComboBox.Text = "Select Province";
            // 
            // phoneTextBox
            // 
            phoneTextBox.BorderStyle = BorderStyle.FixedSingle;
            phoneTextBox.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            phoneTextBox.Location = new Point(254, 232);
            phoneTextBox.Name = "phoneTextBox";
            phoneTextBox.Size = new Size(282, 25);
            phoneTextBox.TabIndex = 10;
            phoneTextBox.Text = "Enter Phone Number";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(66, 159, 132);
            label1.Font = new Font("Arial Narrow", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(338, 35);
            label1.Name = "label1";
            label1.Size = new Size(116, 37);
            label1.TabIndex = 11;
            label1.Text = "Register";
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(79, 184, 156);
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(phoneTextBox);
            Controls.Add(provinceComboBox);
            Controls.Add(schoolComboBox);
            Controls.Add(gradeComboBox);
            Controls.Add(emailTextBox);
            Controls.Add(rePasswordTextBox);
            Controls.Add(passwordTextBox);
            Controls.Add(nameTextBox);
            Controls.Add(button1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
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
        private Button button1;
        private TextBox nameTextBox;
        private TextBox passwordTextBox;
        private TextBox rePasswordTextBox;
        private TextBox emailTextBox;
        private ComboBox gradeComboBox;
        private ComboBox schoolComboBox;
        private ComboBox provinceComboBox;
        private TextBox phoneTextBox;
        private Label label1;
    }
}