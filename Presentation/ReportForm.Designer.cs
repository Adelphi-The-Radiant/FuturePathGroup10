namespace FuturePathGroup10.Presentation
{
    partial class ReportForm
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
            reportTextBox = new TextBox();
            backButton = new Button();
            degreeButton = new Button();
            excpetionButton = new Button();
            exportButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Location = new Point(31, 24);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(735, 395);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Arial Narrow", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(44, 38);
            label1.Name = "label1";
            label1.Size = new Size(72, 29);
            label1.TabIndex = 1;
            label1.Text = "Report";
            // 
            // reportTextBox
            // 
            reportTextBox.BorderStyle = BorderStyle.FixedSingle;
            reportTextBox.Location = new Point(44, 70);
            reportTextBox.Multiline = true;
            reportTextBox.Name = "reportTextBox";
            reportTextBox.Size = new Size(699, 276);
            reportTextBox.TabIndex = 2;
            // 
            // backButton
            // 
            backButton.BackColor = Color.FromArgb(66, 159, 132);
            backButton.FlatStyle = FlatStyle.Popup;
            backButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            backButton.ForeColor = SystemColors.ControlLightLight;
            backButton.Location = new Point(44, 363);
            backButton.Name = "backButton";
            backButton.Size = new Size(147, 40);
            backButton.TabIndex = 3;
            backButton.Text = "Back";
            backButton.UseVisualStyleBackColor = false;
            // 
            // degreeButton
            // 
            degreeButton.BackColor = Color.FromArgb(66, 159, 132);
            degreeButton.FlatStyle = FlatStyle.Popup;
            degreeButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            degreeButton.ForeColor = SystemColors.ControlLightLight;
            degreeButton.Location = new Point(220, 363);
            degreeButton.Name = "degreeButton";
            degreeButton.Size = new Size(160, 40);
            degreeButton.TabIndex = 4;
            degreeButton.Text = "Degree Report";
            degreeButton.UseVisualStyleBackColor = false;
            // 
            // excpetionButton
            // 
            excpetionButton.BackColor = Color.FromArgb(66, 159, 132);
            excpetionButton.FlatStyle = FlatStyle.Popup;
            excpetionButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            excpetionButton.ForeColor = SystemColors.ControlLightLight;
            excpetionButton.Location = new Point(408, 363);
            excpetionButton.Name = "excpetionButton";
            excpetionButton.Size = new Size(166, 40);
            excpetionButton.TabIndex = 5;
            excpetionButton.Text = "Exception Report";
            excpetionButton.UseVisualStyleBackColor = false;
            // 
            // exportButton
            // 
            exportButton.BackColor = Color.FromArgb(66, 159, 132);
            exportButton.FlatStyle = FlatStyle.Popup;
            exportButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            exportButton.ForeColor = SystemColors.ControlLightLight;
            exportButton.Location = new Point(600, 363);
            exportButton.Name = "exportButton";
            exportButton.Size = new Size(143, 40);
            exportButton.TabIndex = 6;
            exportButton.Text = "Export Report";
            exportButton.UseVisualStyleBackColor = false;
            // 
            // ReportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(79, 184, 156);
            ClientSize = new Size(800, 450);
            Controls.Add(exportButton);
            Controls.Add(excpetionButton);
            Controls.Add(degreeButton);
            Controls.Add(backButton);
            Controls.Add(reportTextBox);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "ReportForm";
            Text = "ReportForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private TextBox reportTextBox;
        private Button backButton;
        private Button degreeButton;
        private Button excpetionButton;
        private Button exportButton;
    }
}