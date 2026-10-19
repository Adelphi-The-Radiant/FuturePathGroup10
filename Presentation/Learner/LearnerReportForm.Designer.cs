namespace FuturePathGroup10.Presentation.Learner
{
    partial class LearnerReportForm
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
            summaryTextBox = new TextBox();
            backButton = new Button();
            exportButton = new Button();
            saveFileDialog = new SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Location = new Point(22, 25);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(747, 385);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Arial Narrow", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(33, 39);
            label1.Name = "label1";
            label1.Size = new Size(175, 29);
            label1.TabIndex = 1;
            label1.Text = "Advisory Summary";
            // 
            // summaryTextBox
            // 
            summaryTextBox.BorderStyle = BorderStyle.FixedSingle;
            summaryTextBox.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            summaryTextBox.Location = new Point(33, 71);
            summaryTextBox.Multiline = true;
            summaryTextBox.Name = "summaryTextBox";
            summaryTextBox.ReadOnly = true;
            summaryTextBox.ScrollBars = ScrollBars.Vertical;
            summaryTextBox.Size = new Size(719, 259);
            summaryTextBox.TabIndex = 2;
            // 
            // backButton
            // 
            backButton.BackColor = Color.FromArgb(66, 159, 132);
            backButton.FlatStyle = FlatStyle.Popup;
            backButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            backButton.ForeColor = SystemColors.ControlLightLight;
            backButton.Location = new Point(33, 346);
            backButton.Name = "backButton";
            backButton.Size = new Size(175, 38);
            backButton.TabIndex = 3;
            backButton.Text = "Back";
            backButton.UseVisualStyleBackColor = false;
            backButton.Click += backButton_Click;
            // 
            // exportButton
            // 
            exportButton.BackColor = Color.FromArgb(66, 159, 132);
            exportButton.FlatStyle = FlatStyle.Popup;
            exportButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            exportButton.ForeColor = SystemColors.ControlLightLight;
            exportButton.Location = new Point(530, 346);
            exportButton.Name = "exportButton";
            exportButton.Size = new Size(222, 38);
            exportButton.TabIndex = 4;
            exportButton.Text = "Export Report";
            exportButton.UseVisualStyleBackColor = false;
            exportButton.Click += exportButton_Click;
            // 
            // saveFileDialog
            // 
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            saveFileDialog.Title = "Export Report";
            // 
            // LearnerReportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(79, 184, 156);
            ClientSize = new Size(800, 450);
            Controls.Add(exportButton);
            Controls.Add(backButton);
            Controls.Add(summaryTextBox);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "LearnerReportForm";
            Text = "ReportForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private TextBox summaryTextBox;
        private Button backButton;
        private Button exportButton;
        private SaveFileDialog saveFileDialog;
    }
}