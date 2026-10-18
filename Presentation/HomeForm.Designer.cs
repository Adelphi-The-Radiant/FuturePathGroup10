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
            pictureBox2.Location = new Point(-4, 40);
            pictureBox2.Margin = new Padding(4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(924, 213);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Screenshot_2026_10_14_145905;
            pictureBox3.Location = new Point(14, 71);
            pictureBox3.Margin = new Padding(4);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(146, 148);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 2;
            pictureBox3.TabStop = false;
            // 
            // explainLabel
            // 
            explainLabel.AutoSize = true;
            explainLabel.BackColor = Color.White;
            explainLabel.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            explainLabel.Location = new Point(167, 48);
            explainLabel.Margin = new Padding(4, 0, 4, 0);
            explainLabel.Name = "explainLabel";
            explainLabel.Size = new Size(744, 216);
            explainLabel.TabIndex = 3;
            explainLabel.Text = resources.GetString("explainLabel.Text");
            explainLabel.Click += explainLabel_Click;
            // 
            // marksButton
            // 
            marksButton.Image = Properties.Resources.Screenshot_2026_10_14_145759;
            marksButton.Location = new Point(16, 284);
            marksButton.Margin = new Padding(4);
            marksButton.Name = "marksButton";
            marksButton.Size = new Size(194, 227);
            marksButton.TabIndex = 7;
            marksButton.UseVisualStyleBackColor = true;
            marksButton.Click += marksButton_Click;
            // 
            // tertiaryButton
            // 
            tertiaryButton.Image = Properties.Resources.Screenshot_2026_10_14_145804;
            tertiaryButton.Location = new Point(241, 284);
            tertiaryButton.Margin = new Padding(4);
            tertiaryButton.Name = "tertiaryButton";
            tertiaryButton.Size = new Size(196, 227);
            tertiaryButton.TabIndex = 8;
            tertiaryButton.UseVisualStyleBackColor = true;
            // 
            // fundsButton
            // 
            fundsButton.Image = Properties.Resources.Screenshot_2026_10_14_145815;
            fundsButton.Location = new Point(500, 286);
            fundsButton.Margin = new Padding(4);
            fundsButton.Name = "fundsButton";
            fundsButton.Size = new Size(191, 227);
            fundsButton.TabIndex = 9;
            fundsButton.UseVisualStyleBackColor = true;
            fundsButton.Click += fundsButton_Click;
            // 
            // studReportButton
            // 
            studReportButton.Image = Properties.Resources.Screenshot_2026_10_14_145823;
            studReportButton.Location = new Point(676, 284);
            studReportButton.Margin = new Padding(4);
            studReportButton.Name = "studReportButton";
            studReportButton.Size = new Size(204, 227);
            studReportButton.TabIndex = 10;
            studReportButton.UseVisualStyleBackColor = true;
            // 
            // adminInfoButton
            // 
            adminInfoButton.Image = Properties.Resources.Screenshot_2026_10_14_152123;
            adminInfoButton.Location = new Point(217, 286);
            adminInfoButton.Margin = new Padding(4);
            adminInfoButton.Name = "adminInfoButton";
            adminInfoButton.Size = new Size(196, 227);
            adminInfoButton.TabIndex = 11;
            adminInfoButton.UseVisualStyleBackColor = true;
            // 
            // adminReportButton
            // 
            adminReportButton.Image = Properties.Resources.Screenshot_2026_10_14_145823;
            adminReportButton.Location = new Point(522, 284);
            adminReportButton.Margin = new Padding(4);
            adminReportButton.Name = "adminReportButton";
            adminReportButton.Size = new Size(201, 227);
            adminReportButton.TabIndex = 12;
            adminReportButton.UseVisualStyleBackColor = true;
            // 
            // advisCreateRepButton
            // 
            advisCreateRepButton.Image = Properties.Resources.Screenshot_2026_10_14_152318;
            advisCreateRepButton.Location = new Point(217, 284);
            advisCreateRepButton.Margin = new Padding(4);
            advisCreateRepButton.Name = "advisCreateRepButton";
            advisCreateRepButton.Size = new Size(196, 227);
            advisCreateRepButton.TabIndex = 13;
            advisCreateRepButton.UseVisualStyleBackColor = true;
            // 
            // advisReportButton
            // 
            advisReportButton.Image = Properties.Resources.Screenshot_2026_10_14_145823;
            advisReportButton.Location = new Point(500, 286);
            advisReportButton.Margin = new Padding(4);
            advisReportButton.Name = "advisReportButton";
            advisReportButton.Size = new Size(200, 226);
            advisReportButton.TabIndex = 14;
            advisReportButton.UseVisualStyleBackColor = true;
            // 
            // HomeForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 186, 158);
            ClientSize = new Size(914, 600);
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
            Margin = new Padding(4);
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