namespace FuturePathGroup10.Presentation.Learner
{
    partial class AlternativesForm
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
            listLabel = new Label();
            label3 = new Label();
            backButton = new Button();
            reportButton = new Button();
            fundButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Location = new Point(29, 25);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(744, 387);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Arial Narrow", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(47, 38);
            label1.Name = "label1";
            label1.Size = new Size(176, 29);
            label1.TabIndex = 1;
            label1.Text = "Alternative Options";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(47, 83);
            label2.Name = "label2";
            label2.Size = new Size(454, 20);
            label2.TabIndex = 2;
            label2.Text = "Here are a list of qualifications/fields that you do qualify for based off your interests:";
            // 
            // listLabel
            // 
            listLabel.AutoSize = true;
            listLabel.BackColor = Color.White;
            listLabel.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listLabel.ForeColor = Color.FromArgb(66, 159, 132);
            listLabel.Location = new Point(47, 103);
            listLabel.Name = "listLabel";
            listLabel.Size = new Size(167, 23);
            listLabel.TabIndex = 3;
            listLabel.Text = "Faculty/Qualification list";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.White;
            label3.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(66, 159, 132);
            label3.Location = new Point(47, 310);
            label3.Name = "label3";
            label3.Size = new Size(567, 40);
            label3.TabIndex = 4;
            label3.Text = "You can view possible funding options related to the selected faculty or qualification.\r\nYou can view an advisory summary in the form of a report related to the selected faculty or qualification.\r\n";
            // 
            // backButton
            // 
            backButton.BackColor = Color.FromArgb(66, 159, 132);
            backButton.FlatStyle = FlatStyle.Popup;
            backButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            backButton.ForeColor = SystemColors.ControlLightLight;
            backButton.Location = new Point(47, 353);
            backButton.Name = "backButton";
            backButton.Size = new Size(146, 45);
            backButton.TabIndex = 5;
            backButton.Text = "Back";
            backButton.UseVisualStyleBackColor = false;
            // 
            // reportButton
            // 
            reportButton.BackColor = Color.FromArgb(66, 159, 132);
            reportButton.FlatStyle = FlatStyle.Popup;
            reportButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            reportButton.ForeColor = SystemColors.ControlLightLight;
            reportButton.Location = new Point(304, 353);
            reportButton.Name = "reportButton";
            reportButton.Size = new Size(187, 45);
            reportButton.TabIndex = 6;
            reportButton.Text = "View Report";
            reportButton.UseVisualStyleBackColor = false;
            // 
            // fundButton
            // 
            fundButton.BackColor = Color.FromArgb(66, 159, 132);
            fundButton.FlatStyle = FlatStyle.Popup;
            fundButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            fundButton.ForeColor = SystemColors.ControlLightLight;
            fundButton.Location = new Point(578, 353);
            fundButton.Name = "fundButton";
            fundButton.Size = new Size(181, 45);
            fundButton.TabIndex = 7;
            fundButton.Text = "Funding Options";
            fundButton.UseVisualStyleBackColor = false;
            // 
            // AlternativesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(79, 184, 156);
            ClientSize = new Size(800, 450);
            Controls.Add(fundButton);
            Controls.Add(reportButton);
            Controls.Add(backButton);
            Controls.Add(label3);
            Controls.Add(listLabel);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "AlternativesForm";
            Text = "AlternativesForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private Label listLabel;
        private Label label3;
        private Button backButton;
        private Button reportButton;
        private Button fundButton;
    }
}