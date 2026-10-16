namespace FuturePathGroup10.Presentation
{
    partial class HomeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeForm));
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            explainLabel = new Label();
            marksButton = new Button();
            tertiaryButton = new Button();
            fundsButton = new Button();
            studReportButton = new Button();
            adminInfoButton = new Button();
            adminReportButton = new Button();
            advisCreateRepButton = new Button();
            advisReportButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.White;
            pictureBox2.Location = new Point(-3, 30);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(808, 160);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Screenshot_2026_10_14_145905;
            pictureBox3.Location = new Point(12, 53);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(128, 111);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 2;
            pictureBox3.TabStop = false;
            // 
            // explainLabel
            // 
            explainLabel.AutoSize = true;
            explainLabel.BackColor = Color.White;
            explainLabel.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            explainLabel.Location = new Point(175, 30);
            explainLabel.Name = "explainLabel";
            explainLabel.Size = new Size(598, 160);
            explainLabel.TabIndex = 3;
            explainLabel.Text = resources.GetString("explainLabel.Text");
            // 
            // marksButton
            // 
            marksButton.Image = Properties.Resources.Screenshot_2026_10_14_145759;
            marksButton.Location = new Point(14, 213);
            marksButton.Name = "marksButton";
            marksButton.Size = new Size(170, 170);
            marksButton.TabIndex = 7;
            marksButton.UseVisualStyleBackColor = true;
            // 
            // tertiaryButton
            // 
            tertiaryButton.Image = Properties.Resources.Screenshot_2026_10_14_145804;
            tertiaryButton.Location = new Point(211, 213);
            tertiaryButton.Name = "tertiaryButton";
            tertiaryButton.Size = new Size(171, 170);
            tertiaryButton.TabIndex = 8;
            tertiaryButton.UseVisualStyleBackColor = true;
            // 
            // fundsButton
            // 
            fundsButton.Image = Properties.Resources.Screenshot_2026_10_14_145815;
            fundsButton.Location = new Point(437, 214);
            fundsButton.Name = "fundsButton";
            fundsButton.Size = new Size(167, 170);
            fundsButton.TabIndex = 9;
            fundsButton.UseVisualStyleBackColor = true;
            fundsButton.Click += fundsButton_Click;
            // 
            // studReportButton
            // 
            studReportButton.Image = Properties.Resources.Screenshot_2026_10_14_145823;
            studReportButton.Location = new Point(591, 213);
            studReportButton.Name = "studReportButton";
            studReportButton.Size = new Size(178, 170);
            studReportButton.TabIndex = 10;
            studReportButton.UseVisualStyleBackColor = true;
            // 
            // adminInfoButton
            // 
            adminInfoButton.Image = Properties.Resources.Screenshot_2026_10_14_152123;
            adminInfoButton.Location = new Point(190, 214);
            adminInfoButton.Name = "adminInfoButton";
            adminInfoButton.Size = new Size(172, 170);
            adminInfoButton.TabIndex = 11;
            adminInfoButton.UseVisualStyleBackColor = true;
            // 
            // adminReportButton
            // 
            adminReportButton.Image = Properties.Resources.Screenshot_2026_10_14_145823;
            adminReportButton.Location = new Point(457, 213);
            adminReportButton.Name = "adminReportButton";
            adminReportButton.Size = new Size(176, 170);
            adminReportButton.TabIndex = 12;
            adminReportButton.UseVisualStyleBackColor = true;
            // 
            // advisCreateRepButton
            // 
            advisCreateRepButton.Image = Properties.Resources.Screenshot_2026_10_14_152318;
            advisCreateRepButton.Location = new Point(190, 213);
            advisCreateRepButton.Name = "advisCreateRepButton";
            advisCreateRepButton.Size = new Size(172, 170);
            advisCreateRepButton.TabIndex = 13;
            advisCreateRepButton.UseVisualStyleBackColor = true;
            // 
            // advisReportButton
            // 
            advisReportButton.Image = Properties.Resources.Screenshot_2026_10_14_145823;
            advisReportButton.Location = new Point(437, 214);
            advisReportButton.Name = "advisReportButton";
            advisReportButton.Size = new Size(175, 169);
            advisReportButton.TabIndex = 14;
            advisReportButton.UseVisualStyleBackColor = true;
            // 
            // HomeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 186, 158);
            ClientSize = new Size(800, 450);
            Controls.Add(advisReportButton);
            Controls.Add(advisCreateRepButton);
            Controls.Add(adminReportButton);
            Controls.Add(adminInfoButton);
            Controls.Add(studReportButton);
            Controls.Add(fundsButton);
            Controls.Add(tertiaryButton);
            Controls.Add(marksButton);
            Controls.Add(explainLabel);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Name = "HomeForm";
            Text = "HomeForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Label explainLabel;
        private Button marksButton;
        private Button tertiaryButton;
        private Button fundsButton;
        private Button studReportButton;
        private Button adminInfoButton;
        private Button adminReportButton;
        private Button advisCreateRepButton;
        private Button advisReportButton;
    }
}