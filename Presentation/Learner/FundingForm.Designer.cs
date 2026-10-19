namespace FuturePathGroup10.Presentation.Learner
{
    partial class FundingForm
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
            label1 = new Label();
            label2 = new Label();
            selectionButton = new Label();
            fundingInfoLabel = new Label();
            contactLabel = new Label();
            label3 = new Label();
            backButton = new Button();
            reportButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Location = new Point(54, 58);
            pictureBox1.Margin = new Padding(6, 6, 6, 6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1380, 823);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(80, 92);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(376, 55);
            label1.TabIndex = 1;
            label1.Text = "Funding Options";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(80, 201);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(282, 36);
            label2.TabIndex = 2;
            label2.Text = "Funding options for:";
            // 
            // selectionButton
            // 
            selectionButton.AutoSize = true;
            selectionButton.BackColor = Color.White;
            selectionButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            selectionButton.ForeColor = Color.FromArgb(79, 184, 156);
            selectionButton.Location = new Point(80, 243);
            selectionButton.Margin = new Padding(6, 0, 6, 0);
            selectionButton.Name = "selectionButton";
            selectionButton.Size = new Size(214, 44);
            selectionButton.TabIndex = 3;
            selectionButton.Text = "<selection>";
            // 
            // fundingInfoLabel
            // 
            fundingInfoLabel.AutoSize = true;
            fundingInfoLabel.BackColor = Color.White;
            fundingInfoLabel.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            fundingInfoLabel.Location = new Point(80, 314);
            fundingInfoLabel.Margin = new Padding(6, 0, 6, 0);
            fundingInfoLabel.Name = "fundingInfoLabel";
            fundingInfoLabel.Size = new Size(98, 36);
            fundingInfoLabel.TabIndex = 4;
            fundingInfoLabel.Text = "<info>";
            // 
            // contactLabel
            // 
            contactLabel.AutoSize = true;
            contactLabel.BackColor = Color.White;
            contactLabel.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            contactLabel.ForeColor = Color.FromArgb(66, 159, 132);
            contactLabel.Location = new Point(80, 651);
            contactLabel.Margin = new Padding(6, 0, 6, 0);
            contactLabel.Name = "contactLabel";
            contactLabel.Size = new Size(150, 44);
            contactLabel.TabIndex = 5;
            contactLabel.Text = "Contact";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.White;
            label3.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(66, 159, 132);
            label3.Location = new Point(80, 700);
            label3.Margin = new Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new Size(1365, 36);
            label3.TabIndex = 6;
            label3.Text = "You can view an advisory summary in the form of a report related to the selected faculty or qualification";
            // 
            // backButton
            // 
            backButton.BackColor = Color.FromArgb(66, 159, 132);
            backButton.FlatStyle = FlatStyle.Popup;
            backButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            backButton.ForeColor = SystemColors.ControlLightLight;
            backButton.Location = new Point(89, 749);
            backButton.Margin = new Padding(6, 6, 6, 6);
            backButton.Name = "backButton";
            backButton.Size = new Size(243, 90);
            backButton.TabIndex = 7;
            backButton.Text = "Back";
            backButton.UseVisualStyleBackColor = false;
            backButton.Click += backButton_Click;
            // 
            // reportButton
            // 
            reportButton.BackColor = Color.FromArgb(66, 159, 132);
            reportButton.FlatStyle = FlatStyle.Popup;
            reportButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            reportButton.ForeColor = SystemColors.ControlLightLight;
            reportButton.Location = new Point(1060, 749);
            reportButton.Margin = new Padding(6, 6, 6, 6);
            reportButton.Name = "reportButton";
            reportButton.Size = new Size(342, 90);
            reportButton.TabIndex = 8;
            reportButton.Text = "View Report";
            reportButton.UseVisualStyleBackColor = false;
            // 
            // FundingForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(79, 184, 156);
            ClientSize = new Size(1486, 960);
            Controls.Add(reportButton);
            Controls.Add(backButton);
            Controls.Add(label3);
            Controls.Add(contactLabel);
            Controls.Add(fundingInfoLabel);
            Controls.Add(selectionButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Margin = new Padding(6, 6, 6, 6);
            Name = "FundingForm";
            Text = "FundingForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private Label selectionButton;
        private Label fundingInfoLabel;
        private Label contactLabel;
        private Label label3;
        private Button backButton;
        private Button reportButton;
    }
}