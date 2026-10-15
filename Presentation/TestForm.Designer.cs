namespace FuturePathGroup10.Presentation
{
    partial class TestForm
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
            testButton = new Button();
            SuspendLayout();
            // 
            // testButton
            // 
            testButton.Location = new Point(181, 344);
            testButton.Name = "testButton";
            testButton.Size = new Size(94, 29);
            testButton.TabIndex = 0;
            testButton.Text = "save";
            testButton.UseVisualStyleBackColor = true;
            testButton.Click += testButton_Click;
            // 
            // TestForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(testButton);
            Name = "TestForm";
            Text = "TestForm";
            ResumeLayout(false);
        }

        #endregion

        private Button testButton;
    }
}