namespace InputFileEditor
{
    partial class PressAnyKey
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
            this.pressAnyLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pressAnyLabel
            // 
            this.pressAnyLabel.AutoSize = true;
            this.pressAnyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.pressAnyLabel.Location = new System.Drawing.Point(46, 9);
            this.pressAnyLabel.Name = "pressAnyLabel";
            this.pressAnyLabel.Size = new System.Drawing.Size(189, 20);
            this.pressAnyLabel.TabIndex = 0;
            this.pressAnyLabel.Text = "Press any key to continue";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(103, 52);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // PressAnyKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 87);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.pressAnyLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PressAnyKey";
            this.Text = "Press Any Key";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label pressAnyLabel;
        private System.Windows.Forms.Button cancelButton;
    }
}