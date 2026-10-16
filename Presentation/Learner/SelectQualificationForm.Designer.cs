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
            pictureBox1.Location = new Point(40, 27);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(726, 381);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.BackColor = Color.White;
            titleLabel.Font = new Font("Arial Narrow", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            titleLabel.Location = new Point(59, 45);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(151, 29);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "Tertiary Options";
            // 
            // subtitleLabel
            // 
            subtitleLabel.AutoSize = true;
            subtitleLabel.BackColor = Color.White;
            subtitleLabel.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            subtitleLabel.Location = new Point(59, 74);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Size = new Size(350, 20);
            subtitleLabel.TabIndex = 2;
            subtitleLabel.Text = "Check your eligibility status for any faculty, qualification, or both";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(79, 184, 156);
            label1.Location = new Point(59, 94);
            label1.Name = "label1";
            label1.Size = new Size(323, 20);
            label1.TabIndex = 3;
            label1.Text = "*You must have your marks entered before you can check";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(79, 184, 156);
            label2.Location = new Point(59, 130);
            label2.Name = "label2";
            label2.Size = new Size(105, 23);
            label2.TabIndex = 4;
            label2.Text = "Select Faculty";
            // 
            // facultyComboBox
            // 
            facultyComboBox.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            facultyComboBox.FormattingEnabled = true;
            facultyComboBox.Location = new Point(59, 156);
            facultyComboBox.Name = "facultyComboBox";
            facultyComboBox.Size = new Size(474, 28);
            facultyComboBox.TabIndex = 5;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Screenshot_15_10_2026_124343_docs_google_com;
            pictureBox2.Location = new Point(565, 130);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(162, 174);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.White;
            label3.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(79, 184, 156);
            label3.Location = new Point(59, 220);
            label3.Name = "label3";
            label3.Size = new Size(139, 23);
            label3.TabIndex = 7;
            label3.Text = "Select Qualificaiton";
            // 
            // qualificationComboBox
            // 
            qualificationComboBox.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            qualificationComboBox.FormattingEnabled = true;
            qualificationComboBox.Location = new Point(59, 246);
            qualificationComboBox.Name = "qualificationComboBox";
            qualificationComboBox.Size = new Size(474, 28);
            qualificationComboBox.TabIndex = 8;
            // 
            // backButton
            // 
            backButton.BackColor = Color.FromArgb(66, 159, 132);
            backButton.FlatStyle = FlatStyle.Popup;
            backButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            backButton.ForeColor = SystemColors.ControlLightLight;
            backButton.Location = new Point(59, 346);
            backButton.Name = "backButton";
            backButton.Size = new Size(139, 43);
            backButton.TabIndex = 9;
            backButton.Text = "Back";
            backButton.UseVisualStyleBackColor = false;
            // 
            // beginButton
            // 
            beginButton.BackColor = Color.FromArgb(66, 159, 132);
            beginButton.FlatStyle = FlatStyle.Popup;
            beginButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            beginButton.ForeColor = SystemColors.ControlLightLight;
            beginButton.Location = new Point(522, 346);
            beginButton.Name = "beginButton";
            beginButton.Size = new Size(205, 43);
            beginButton.TabIndex = 10;
            beginButton.Text = "Begin Questionnaire";
            beginButton.UseVisualStyleBackColor = false;
            // 
            // SelectQualificationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(79, 184, 156);
            ClientSize = new Size(800, 450);
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