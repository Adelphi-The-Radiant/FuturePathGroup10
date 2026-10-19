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
            pictureBox1.Location = new Point(54, 53);
            pictureBox1.Margin = new Padding(6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1382, 826);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(87, 81);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(428, 55);
            label1.TabIndex = 1;
            label1.Text = "Alternative Options";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(87, 177);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(1109, 36);
            label2.TabIndex = 2;
            label2.Text = "Here is a list of other qualifications that you could qualify for based on your marks:";
            // 
            // listLabel
            // 
            listLabel.AutoSize = true;
            listLabel.BackColor = Color.White;
            listLabel.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listLabel.ForeColor = Color.FromArgb(66, 159, 132);
            listLabel.Location = new Point(87, 220);
            listLabel.Margin = new Padding(6, 0, 6, 0);
            listLabel.Name = "listLabel";
            listLabel.Size = new Size(423, 44);
            listLabel.TabIndex = 3;
            listLabel.Text = "Faculty/Qualification list";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.White;
            label3.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(66, 159, 132);
            label3.Location = new Point(87, 661);
            label3.Margin = new Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new Size(1373, 72);
            label3.TabIndex = 4;
            label3.Text = "You can view possible funding options related to the selected faculty or qualification.\r\nYou can view an advisory summary in the form of a report related to the selected faculty or qualification.\r\n";
            // 
            // backButton
            // 
            backButton.BackColor = Color.FromArgb(66, 159, 132);
            backButton.FlatStyle = FlatStyle.Popup;
            backButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            backButton.ForeColor = SystemColors.ControlLightLight;
            backButton.Location = new Point(87, 753);
            backButton.Margin = new Padding(6);
            backButton.Name = "backButton";
            backButton.Size = new Size(271, 96);
            backButton.TabIndex = 5;
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
            reportButton.Location = new Point(565, 753);
            reportButton.Margin = new Padding(6);
            reportButton.Name = "reportButton";
            reportButton.Size = new Size(347, 96);
            reportButton.TabIndex = 6;
            reportButton.Text = "View Report";
            reportButton.UseVisualStyleBackColor = false;
            reportButton.Click += reportButton_Click;
            // 
            // fundButton
            // 
            fundButton.BackColor = Color.FromArgb(66, 159, 132);
            fundButton.FlatStyle = FlatStyle.Popup;
            fundButton.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            fundButton.ForeColor = SystemColors.ControlLightLight;
            fundButton.Location = new Point(1073, 753);
            fundButton.Margin = new Padding(6);
            fundButton.Name = "fundButton";
            fundButton.Size = new Size(336, 96);
            fundButton.TabIndex = 7;
            fundButton.Text = "Funding Options";
            fundButton.UseVisualStyleBackColor = false;
            fundButton.Click += fundButton_Click;
            // 
            // AlternativesForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(79, 184, 156);
            ClientSize = new Size(1486, 960);
            Controls.Add(fundButton);
            Controls.Add(reportButton);
            Controls.Add(backButton);
            Controls.Add(label3);
            Controls.Add(listLabel);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Margin = new Padding(6);
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