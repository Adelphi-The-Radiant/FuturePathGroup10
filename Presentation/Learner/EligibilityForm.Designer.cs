namespace FuturePathGroup10.Presentation.Learner
{
    partial class EligibilityForm
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
            eligibilityLabel = new Label();
            eligibleSubtitleLabel = new Label();
            selectionLabel = new Label();
            label1 = new Label();
            statusLabel = new Label();
            yapLabel = new Label();
            detailsTextBox = new TextBox();
            label2 = new Label();
            backButton = new Button();
            reportButton = new Button();
            fundButton = new Button();
            alternativeButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Location = new Point(36, 26);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(724, 389);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // eligibilityLabel
            // 
            eligibilityLabel.AutoSize = true;
            eligibilityLabel.BackColor = Color.White;
            eligibilityLabel.Font = new Font("Arial Narrow", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            eligibilityLabel.Location = new Point(51, 43);
            eligibilityLabel.Name = "eligibilityLabel";
            eligibilityLabel.Size = new Size(108, 29);
            eligibilityLabel.TabIndex = 1;
            eligibilityLabel.Text = "<eligibility>";
            // 
            // eligibleSubtitleLabel
            // 
            eligibleSubtitleLabel.AutoSize = true;
            eligibleSubtitleLabel.BackColor = Color.White;
            eligibleSubtitleLabel.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            eligibleSubtitleLabel.Location = new Point(51, 74);
            eligibleSubtitleLabel.Name = "eligibleSubtitleLabel";
            eligibleSubtitleLabel.Size = new Size(95, 20);
            eligibleSubtitleLabel.TabIndex = 2;
            eligibleSubtitleLabel.Text = "\\congrats twin :)";
            // 
            // selectionLabel
            // 
            selectionLabel.AutoSize = true;
            selectionLabel.BackColor = Color.White;
            selectionLabel.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            selectionLabel.ForeColor = Color.FromArgb(79, 184, 156);
            selectionLabel.Location = new Point(56, 94);
            selectionLabel.Name = "selectionLabel";
            selectionLabel.Size = new Size(96, 23);
            selectionLabel.TabIndex = 3;
            selectionLabel.Text = "Faculty/Qual";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Location = new Point(56, 145);
            label1.Name = "label1";
            label1.Size = new Size(130, 15);
            label1.TabIndex = 4;
            label1.Text = "Your eligibility status is:";
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.BackColor = Color.White;
            statusLabel.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            statusLabel.ForeColor = Color.FromArgb(79, 184, 156);
            statusLabel.Location = new Point(56, 160);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(68, 23);
            statusLabel.TabIndex = 5;
            statusLabel.Text = "<status>";
            // 
            // yapLabel
            // 
            yapLabel.AutoSize = true;
            yapLabel.BackColor = Color.White;
            yapLabel.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            yapLabel.Location = new Point(56, 194);
            yapLabel.Name = "yapLabel";
            yapLabel.Size = new Size(42, 20);
            yapLabel.TabIndex = 6;
            yapLabel.Text = "<yap>";
            yapLabel.Visible = false;
            // 
            // detailsTextBox
            // 
            detailsTextBox.BackColor = Color.White;
            detailsTextBox.BorderStyle = BorderStyle.None;
            detailsTextBox.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            detailsTextBox.Location = new Point(56, 194);
            detailsTextBox.Multiline = true;
            detailsTextBox.Name = "detailsTextBox";
            detailsTextBox.ReadOnly = true;
            detailsTextBox.ScrollBars = ScrollBars.Vertical;
            detailsTextBox.Size = new Size(680, 70);
            detailsTextBox.TabIndex = 12;
            detailsTextBox.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(66, 159, 132);
            label2.Location = new Point(56, 314);
            label2.Name = "label2";
            label2.Size = new Size(567, 40);
            label2.TabIndex = 7;
            label2.Text = "You can view possible funding options related to the selected faculty or qualification.\r\nYou can view an advisory summary in the form of a report related to the selected faculty or qualification.\r\n";
            // 
            // backButton
            // 
            backButton.BackColor = Color.FromArgb(66, 159, 132);
            backButton.FlatStyle = FlatStyle.Popup;
            backButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            backButton.ForeColor = SystemColors.ControlLightLight;
            backButton.Location = new Point(56, 357);
            backButton.Name = "backButton";
            backButton.Size = new Size(130, 40);
            backButton.TabIndex = 8;
            backButton.Text = "Back";
            backButton.UseVisualStyleBackColor = false;
            backButton.Click += backButton_Click;
            // 
            // reportButton
            // 
            reportButton.BackColor = Color.FromArgb(66, 159, 132);
            reportButton.FlatStyle = FlatStyle.Popup;
            reportButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            reportButton.ForeColor = SystemColors.ControlLightLight;
            reportButton.Location = new Point(314, 357);
            reportButton.Name = "reportButton";
            reportButton.Size = new Size(166, 40);
            reportButton.TabIndex = 9;
            reportButton.Text = "View Report";
            reportButton.UseVisualStyleBackColor = false;
            reportButton.Click += reportButton_Click;
            // 
            // fundButton
            // 
            fundButton.BackColor = Color.FromArgb(66, 159, 132);
            fundButton.FlatStyle = FlatStyle.Popup;
            fundButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            fundButton.ForeColor = SystemColors.ControlLightLight;
            fundButton.Location = new Point(560, 357);
            fundButton.Name = "fundButton";
            fundButton.Size = new Size(178, 40);
            fundButton.TabIndex = 10;
            fundButton.Text = "Funding Options";
            fundButton.UseVisualStyleBackColor = false;
            fundButton.Click += fundButton_Click;
            // 
            // alternativeButton
            // 
            alternativeButton.BackColor = Color.FromArgb(66, 159, 132);
            alternativeButton.FlatStyle = FlatStyle.Popup;
            alternativeButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            alternativeButton.ForeColor = SystemColors.ControlLightLight;
            alternativeButton.Location = new Point(56, 248);
            alternativeButton.Name = "alternativeButton";
            alternativeButton.Size = new Size(176, 41);
            alternativeButton.TabIndex = 11;
            alternativeButton.Text = "Alternative Options";
            alternativeButton.UseVisualStyleBackColor = false;
            alternativeButton.Click += alternativeButton_Click;
            // 
            // EligibilityForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(79, 184, 156);
            ClientSize = new Size(800, 450);
            Controls.Add(detailsTextBox);
            Controls.Add(alternativeButton);
            Controls.Add(fundButton);
            Controls.Add(reportButton);
            Controls.Add(backButton);
            Controls.Add(label2);
            Controls.Add(yapLabel);
            Controls.Add(statusLabel);
            Controls.Add(label1);
            Controls.Add(selectionLabel);
            Controls.Add(eligibleSubtitleLabel);
            Controls.Add(eligibilityLabel);
            Controls.Add(pictureBox1);
            Name = "EligibilityForm";
            Text = "EligibilityForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label eligibilityLabel;
        private Label eligibleSubtitleLabel;
        private Label selectionLabel;
        private Label label1;
        private Label statusLabel;
        private Label yapLabel;
        private TextBox detailsTextBox;
        private Label label2;
        private Button backButton;
        private Button reportButton;
        private Button fundButton;
        private Button alternativeButton;
    }
}