namespace FuturePathGroup10.Presentation.Learner
{
    partial class SelectQualificationForm
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
            titleLabel = new Label();
            subtitleLabel = new Label();
            label1 = new Label();
            label2 = new Label();
            facultyComboBox = new ComboBox();
            pictureBox2 = new PictureBox();
            label3 = new Label();
            qualificationComboBox = new ComboBox();
            backButton = new Button();
            beginButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Location = new Point(46, 36);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(830, 508);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.BackColor = Color.White;
            titleLabel.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            titleLabel.Location = new Point(67, 60);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(226, 36);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "Tertiary Options";
            // 
            // subtitleLabel
            // 
            subtitleLabel.AutoSize = true;
            subtitleLabel.BackColor = Color.White;
            subtitleLabel.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            subtitleLabel.Location = new Point(67, 99);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Size = new Size(528, 24);
            subtitleLabel.TabIndex = 2;
            subtitleLabel.Text = "Check your eligibility status for any faculty, qualification, or both";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(79, 184, 156);
            label1.Location = new Point(67, 125);
            label1.Name = "label1";
            label1.Size = new Size(496, 24);
            label1.TabIndex = 3;
            label1.Text = "*You must have your marks entered before you can check";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(79, 184, 156);
            label2.Location = new Point(67, 173);
            label2.Name = "label2";
            label2.Size = new Size(163, 29);
            label2.TabIndex = 4;
            label2.Text = "Select Faculty";
            // 
            // facultyComboBox
            // 
            facultyComboBox.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            facultyComboBox.FormattingEnabled = true;
            facultyComboBox.Location = new Point(67, 208);
            facultyComboBox.Margin = new Padding(3, 4, 3, 4);
            facultyComboBox.Name = "facultyComboBox";
            facultyComboBox.Size = new Size(541, 32);
            facultyComboBox.TabIndex = 5;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Screenshot_15_10_2026_124343_docs_google_com;
            pictureBox2.Location = new Point(646, 173);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(185, 232);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.White;
            label3.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(79, 184, 156);
            label3.Location = new Point(67, 293);
            label3.Name = "label3";
            label3.Size = new Size(220, 29);
            label3.TabIndex = 7;
            label3.Text = "Select Qualification";
            // 
            // qualificationComboBox
            // 
            qualificationComboBox.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            qualificationComboBox.FormattingEnabled = true;
            qualificationComboBox.Location = new Point(67, 328);
            qualificationComboBox.Margin = new Padding(3, 4, 3, 4);
            qualificationComboBox.Name = "qualificationComboBox";
            qualificationComboBox.Size = new Size(541, 32);
            qualificationComboBox.TabIndex = 8;
            // 
            // backButton
            // 
            backButton.BackColor = Color.FromArgb(66, 159, 132);
            backButton.FlatStyle = FlatStyle.Popup;
            backButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            backButton.ForeColor = SystemColors.ControlLightLight;
            backButton.Location = new Point(67, 461);
            backButton.Margin = new Padding(3, 4, 3, 4);
            backButton.Name = "backButton";
            backButton.Size = new Size(159, 57);
            backButton.TabIndex = 9;
            backButton.Text = "Back";
            backButton.UseVisualStyleBackColor = false;
            backButton.Click += backButton_Click;
            // 
            // beginButton
            // 
            beginButton.BackColor = Color.FromArgb(66, 159, 132);
            beginButton.FlatStyle = FlatStyle.Popup;
            beginButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            beginButton.ForeColor = SystemColors.ControlLightLight;
            beginButton.Location = new Point(597, 461);
            beginButton.Margin = new Padding(3, 4, 3, 4);
            beginButton.Name = "beginButton";
            beginButton.Size = new Size(234, 57);
            beginButton.TabIndex = 10;
            beginButton.Text = "Begin Questionnaire";
            beginButton.UseVisualStyleBackColor = false;
            beginButton.Click += beginButton_Click;
            // 
            // SelectQualificationForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(79, 184, 156);
            ClientSize = new Size(914, 600);
            Controls.Add(beginButton);
            Controls.Add(backButton);
            Controls.Add(qualificationComboBox);
            Controls.Add(label3);
            Controls.Add(pictureBox2);
            Controls.Add(facultyComboBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(subtitleLabel);
            Controls.Add(titleLabel);
            Controls.Add(pictureBox1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "SelectQualificationForm";
            Text = "SelectQualificationForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label titleLabel;
        private Label subtitleLabel;
        private Label label1;
        private Label label2;
        private ComboBox facultyComboBox;
        private PictureBox pictureBox2;
        private Label label3;
        private ComboBox qualificationComboBox;
        private Button backButton;
        private Button beginButton;
    }
}