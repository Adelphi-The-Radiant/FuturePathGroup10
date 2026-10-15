namespace FuturePathGroup10.Presentation.Advisory
{
    partial class GenerateReportForm
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
            comboBox1 = new ComboBox();
            backButton = new Button();
            createButton = new Button();
            reportTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Location = new Point(37, 30);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(730, 382);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Arial Narrow", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(49, 44);
            label1.Name = "label1";
            label1.Size = new Size(136, 29);
            label1.TabIndex = 1;
            label1.Text = "Create Report";
            // 
            // comboBox1
            // 
            comboBox1.BackColor = Color.FromArgb(66, 159, 132);
            comboBox1.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.ForeColor = SystemColors.Window;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(537, 42);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(217, 31);
            comboBox1.TabIndex = 2;
            comboBox1.Text = "Select Student";
            // 
            // backButton
            // 
            backButton.BackColor = Color.FromArgb(66, 159, 132);
            backButton.FlatStyle = FlatStyle.Popup;
            backButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            backButton.ForeColor = SystemColors.ControlLightLight;
            backButton.Location = new Point(49, 341);
            backButton.Name = "backButton";
            backButton.Size = new Size(145, 48);
            backButton.TabIndex = 3;
            backButton.Text = "Back";
            backButton.UseVisualStyleBackColor = false;
            // 
            // createButton
            // 
            createButton.BackColor = Color.FromArgb(66, 159, 132);
            createButton.FlatStyle = FlatStyle.Popup;
            createButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            createButton.ForeColor = SystemColors.ControlLightLight;
            createButton.Location = new Point(609, 341);
            createButton.Name = "createButton";
            createButton.Size = new Size(145, 48);
            createButton.TabIndex = 4;
            createButton.Text = "Create";
            createButton.UseVisualStyleBackColor = false;
            // 
            // reportTextBox
            // 
            reportTextBox.BackColor = SystemColors.Window;
            reportTextBox.BorderStyle = BorderStyle.FixedSingle;
            reportTextBox.Location = new Point(49, 76);
            reportTextBox.Multiline = true;
            reportTextBox.Name = "reportTextBox";
            reportTextBox.Size = new Size(705, 259);
            reportTextBox.TabIndex = 5;
            // 
            // GenerateReportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(79, 184, 156);
            ClientSize = new Size(800, 450);
            Controls.Add(reportTextBox);
            Controls.Add(createButton);
            Controls.Add(backButton);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "GenerateReportForm";
            Text = "GenerateReportForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private ComboBox comboBox1;
        private Button backButton;
        private Button createButton;
        private TextBox reportTextBox;
    }
}