namespace FuturePathGroup10.Presentation.Administration
{
    partial class ViewInformationForm
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
            dataTextBox = new TextBox();
            backButton = new Button();
            insertButton = new Button();
            updateButton = new Button();
            deleteButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Location = new Point(32, 25);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(736, 394);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Arial Narrow", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(50, 40);
            label1.Name = "label1";
            label1.Size = new Size(54, 29);
            label1.TabIndex = 1;
            label1.Text = "Data";
            // 
            // dataTextBox
            // 
            dataTextBox.BorderStyle = BorderStyle.FixedSingle;
            dataTextBox.Font = new Font("Arial Narrow", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataTextBox.Location = new Point(50, 72);
            dataTextBox.Multiline = true;
            dataTextBox.Name = "dataTextBox";
            dataTextBox.Size = new Size(701, 285);
            dataTextBox.TabIndex = 2;
            // 
            // backButton
            // 
            backButton.BackColor = Color.FromArgb(66, 159, 132);
            backButton.FlatStyle = FlatStyle.Popup;
            backButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            backButton.ForeColor = SystemColors.ControlLightLight;
            backButton.Location = new Point(50, 363);
            backButton.Name = "backButton";
            backButton.Size = new Size(158, 44);
            backButton.TabIndex = 3;
            backButton.Text = "Back";
            backButton.UseVisualStyleBackColor = false;
            // 
            // insertButton
            // 
            insertButton.BackColor = Color.FromArgb(66, 159, 132);
            insertButton.FlatStyle = FlatStyle.Popup;
            insertButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            insertButton.ForeColor = SystemColors.ControlLightLight;
            insertButton.Location = new Point(232, 363);
            insertButton.Name = "insertButton";
            insertButton.Size = new Size(146, 44);
            insertButton.TabIndex = 4;
            insertButton.Text = "Insert";
            insertButton.UseVisualStyleBackColor = false;
            // 
            // updateButton
            // 
            updateButton.BackColor = Color.FromArgb(66, 159, 132);
            updateButton.FlatStyle = FlatStyle.Popup;
            updateButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            updateButton.ForeColor = SystemColors.ControlLightLight;
            updateButton.Location = new Point(415, 363);
            updateButton.Name = "updateButton";
            updateButton.Size = new Size(154, 44);
            updateButton.TabIndex = 5;
            updateButton.Text = "Update";
            updateButton.UseVisualStyleBackColor = false;
            // 
            // deleteButton
            // 
            deleteButton.BackColor = Color.FromArgb(66, 159, 132);
            deleteButton.FlatStyle = FlatStyle.Popup;
            deleteButton.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            deleteButton.ForeColor = SystemColors.ControlLightLight;
            deleteButton.Location = new Point(613, 363);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(138, 44);
            deleteButton.TabIndex = 6;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = false;
            // 
            // ViewInformationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(79, 184, 156);
            ClientSize = new Size(800, 450);
            Controls.Add(deleteButton);
            Controls.Add(updateButton);
            Controls.Add(insertButton);
            Controls.Add(backButton);
            Controls.Add(dataTextBox);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "ViewInformationForm";
            Text = "ViewInformationForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private TextBox dataTextBox;
        private Button backButton;
        private Button insertButton;
        private Button updateButton;
        private Button deleteButton;
    }
}