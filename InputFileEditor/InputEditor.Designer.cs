namespace InputFileEditor
{
    partial class InputEditor
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
            this.frameBox = new System.Windows.Forms.ListBox();
            this.framePageControl = new System.Windows.Forms.TabControl();
            this.general = new System.Windows.Forms.TabPage();
            this.frameLengthLabel = new System.Windows.Forms.Label();
            this.frameLengthInput = new System.Windows.Forms.NumericUpDown();
            this.keyboard = new System.Windows.Forms.TabPage();
            this.keysBox = new System.Windows.Forms.ListBox();
            this.mouse = new System.Windows.Forms.TabPage();
            this.gamepad1 = new System.Windows.Forms.TabPage();
            this.gamepad2 = new System.Windows.Forms.TabPage();
            this.gamepad3 = new System.Windows.Forms.TabPage();
            this.gamepad4 = new System.Windows.Forms.TabPage();
            this.addKeyButton = new System.Windows.Forms.Button();
            this.removeKeyButton = new System.Windows.Forms.Button();
            this.mouseXInput = new System.Windows.Forms.NumericUpDown();
            this.mouseYInput = new System.Windows.Forms.NumericUpDown();
            this.mouseScrollInput = new System.Windows.Forms.NumericUpDown();
            this.mouseXLabel = new System.Windows.Forms.Label();
            this.mouseYLabel = new System.Windows.Forms.Label();
            this.mouseScrollLabel = new System.Windows.Forms.Label();
            this.mouseLeftBox = new System.Windows.Forms.CheckBox();
            this.mouseMiddleBox = new System.Windows.Forms.CheckBox();
            this.mouseRightBox = new System.Windows.Forms.CheckBox();
            this.mouse4Box = new System.Windows.Forms.CheckBox();
            this.mouse5Box = new System.Windows.Forms.CheckBox();
            this.addFrameButton = new System.Windows.Forms.Button();
            this.removeFrameButton = new System.Windows.Forms.Button();
            this.framePageControl.SuspendLayout();
            this.general.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frameLengthInput)).BeginInit();
            this.keyboard.SuspendLayout();
            this.mouse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mouseXInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mouseYInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mouseScrollInput)).BeginInit();
            this.SuspendLayout();
            // 
            // frameBox
            // 
            this.frameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.frameBox.FormattingEnabled = true;
            this.frameBox.ItemHeight = 20;
            this.frameBox.Location = new System.Drawing.Point(12, 12);
            this.frameBox.Name = "frameBox";
            this.frameBox.Size = new System.Drawing.Size(140, 364);
            this.frameBox.TabIndex = 1;
            this.frameBox.SelectedIndexChanged += new System.EventHandler(this.frameBox_SelectedIndexChanged);
            // 
            // framePageControl
            // 
            this.framePageControl.Controls.Add(this.general);
            this.framePageControl.Controls.Add(this.keyboard);
            this.framePageControl.Controls.Add(this.mouse);
            this.framePageControl.Controls.Add(this.gamepad1);
            this.framePageControl.Controls.Add(this.gamepad2);
            this.framePageControl.Controls.Add(this.gamepad3);
            this.framePageControl.Controls.Add(this.gamepad4);
            this.framePageControl.Location = new System.Drawing.Point(158, 12);
            this.framePageControl.Name = "framePageControl";
            this.framePageControl.SelectedIndex = 0;
            this.framePageControl.Size = new System.Drawing.Size(422, 426);
            this.framePageControl.TabIndex = 2;
            // 
            // general
            // 
            this.general.Controls.Add(this.frameLengthLabel);
            this.general.Controls.Add(this.frameLengthInput);
            this.general.Location = new System.Drawing.Point(4, 22);
            this.general.Name = "general";
            this.general.Padding = new System.Windows.Forms.Padding(3);
            this.general.Size = new System.Drawing.Size(414, 400);
            this.general.TabIndex = 0;
            this.general.Text = "General";
            this.general.UseVisualStyleBackColor = true;
            // 
            // frameLengthLabel
            // 
            this.frameLengthLabel.AutoSize = true;
            this.frameLengthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.frameLengthLabel.Location = new System.Drawing.Point(6, 8);
            this.frameLengthLabel.Name = "frameLengthLabel";
            this.frameLengthLabel.Size = new System.Drawing.Size(67, 20);
            this.frameLengthLabel.TabIndex = 1;
            this.frameLengthLabel.Text = "Length: ";
            // 
            // frameLengthInput
            // 
            this.frameLengthInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.frameLengthInput.Location = new System.Drawing.Point(79, 6);
            this.frameLengthInput.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.frameLengthInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.frameLengthInput.Name = "frameLengthInput";
            this.frameLengthInput.Size = new System.Drawing.Size(120, 26);
            this.frameLengthInput.TabIndex = 0;
            this.frameLengthInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // keyboard
            // 
            this.keyboard.Controls.Add(this.removeKeyButton);
            this.keyboard.Controls.Add(this.addKeyButton);
            this.keyboard.Controls.Add(this.keysBox);
            this.keyboard.Location = new System.Drawing.Point(4, 22);
            this.keyboard.Name = "keyboard";
            this.keyboard.Padding = new System.Windows.Forms.Padding(3);
            this.keyboard.Size = new System.Drawing.Size(414, 400);
            this.keyboard.TabIndex = 1;
            this.keyboard.Text = "Keyboard";
            this.keyboard.UseVisualStyleBackColor = true;
            // 
            // keysBox
            // 
            this.keysBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.keysBox.FormattingEnabled = true;
            this.keysBox.ItemHeight = 20;
            this.keysBox.Location = new System.Drawing.Point(6, 6);
            this.keysBox.Name = "keysBox";
            this.keysBox.Size = new System.Drawing.Size(120, 324);
            this.keysBox.TabIndex = 0;
            // 
            // mouse
            // 
            this.mouse.Controls.Add(this.mouse5Box);
            this.mouse.Controls.Add(this.mouse4Box);
            this.mouse.Controls.Add(this.mouseRightBox);
            this.mouse.Controls.Add(this.mouseMiddleBox);
            this.mouse.Controls.Add(this.mouseLeftBox);
            this.mouse.Controls.Add(this.mouseScrollLabel);
            this.mouse.Controls.Add(this.mouseYLabel);
            this.mouse.Controls.Add(this.mouseXLabel);
            this.mouse.Controls.Add(this.mouseScrollInput);
            this.mouse.Controls.Add(this.mouseYInput);
            this.mouse.Controls.Add(this.mouseXInput);
            this.mouse.Location = new System.Drawing.Point(4, 22);
            this.mouse.Name = "mouse";
            this.mouse.Size = new System.Drawing.Size(414, 400);
            this.mouse.TabIndex = 2;
            this.mouse.Text = "Mouse";
            this.mouse.UseVisualStyleBackColor = true;
            // 
            // gamepad1
            // 
            this.gamepad1.Location = new System.Drawing.Point(4, 22);
            this.gamepad1.Name = "gamepad1";
            this.gamepad1.Size = new System.Drawing.Size(414, 400);
            this.gamepad1.TabIndex = 3;
            this.gamepad1.Text = "Gamepad 1";
            this.gamepad1.UseVisualStyleBackColor = true;
            // 
            // gamepad2
            // 
            this.gamepad2.Location = new System.Drawing.Point(4, 22);
            this.gamepad2.Name = "gamepad2";
            this.gamepad2.Size = new System.Drawing.Size(414, 400);
            this.gamepad2.TabIndex = 4;
            this.gamepad2.Text = "Gamepad 2";
            this.gamepad2.UseVisualStyleBackColor = true;
            // 
            // gamepad3
            // 
            this.gamepad3.Location = new System.Drawing.Point(4, 22);
            this.gamepad3.Name = "gamepad3";
            this.gamepad3.Size = new System.Drawing.Size(414, 400);
            this.gamepad3.TabIndex = 5;
            this.gamepad3.Text = "Gamepad 3";
            this.gamepad3.UseVisualStyleBackColor = true;
            // 
            // gamepad4
            // 
            this.gamepad4.Location = new System.Drawing.Point(4, 22);
            this.gamepad4.Name = "gamepad4";
            this.gamepad4.Size = new System.Drawing.Size(414, 400);
            this.gamepad4.TabIndex = 6;
            this.gamepad4.Text = "Gamepad 4";
            this.gamepad4.UseVisualStyleBackColor = true;
            // 
            // addKeyButton
            // 
            this.addKeyButton.Location = new System.Drawing.Point(6, 342);
            this.addKeyButton.Name = "addKeyButton";
            this.addKeyButton.Size = new System.Drawing.Size(119, 23);
            this.addKeyButton.TabIndex = 1;
            this.addKeyButton.Text = "Add Key";
            this.addKeyButton.UseVisualStyleBackColor = true;
            this.addKeyButton.Click += new System.EventHandler(this.addKeyButton_Click);
            // 
            // removeKeyButton
            // 
            this.removeKeyButton.Location = new System.Drawing.Point(6, 371);
            this.removeKeyButton.Name = "removeKeyButton";
            this.removeKeyButton.Size = new System.Drawing.Size(119, 23);
            this.removeKeyButton.TabIndex = 2;
            this.removeKeyButton.Text = "Remove Key";
            this.removeKeyButton.UseVisualStyleBackColor = true;
            this.removeKeyButton.Click += new System.EventHandler(this.removeKeyButton_Click);
            // 
            // mouseXInput
            // 
            this.mouseXInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.mouseXInput.Location = new System.Drawing.Point(64, 6);
            this.mouseXInput.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.mouseXInput.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.mouseXInput.Name = "mouseXInput";
            this.mouseXInput.Size = new System.Drawing.Size(120, 26);
            this.mouseXInput.TabIndex = 0;
            // 
            // mouseYInput
            // 
            this.mouseYInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.mouseYInput.Location = new System.Drawing.Point(64, 38);
            this.mouseYInput.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.mouseYInput.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.mouseYInput.Name = "mouseYInput";
            this.mouseYInput.Size = new System.Drawing.Size(120, 26);
            this.mouseYInput.TabIndex = 1;
            // 
            // mouseScrollInput
            // 
            this.mouseScrollInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.mouseScrollInput.Location = new System.Drawing.Point(64, 70);
            this.mouseScrollInput.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.mouseScrollInput.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.mouseScrollInput.Name = "mouseScrollInput";
            this.mouseScrollInput.Size = new System.Drawing.Size(120, 26);
            this.mouseScrollInput.TabIndex = 2;
            // 
            // mouseXLabel
            // 
            this.mouseXLabel.AutoSize = true;
            this.mouseXLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.mouseXLabel.Location = new System.Drawing.Point(6, 8);
            this.mouseXLabel.Name = "mouseXLabel";
            this.mouseXLabel.Size = new System.Drawing.Size(24, 20);
            this.mouseXLabel.TabIndex = 3;
            this.mouseXLabel.Text = "X:";
            // 
            // mouseYLabel
            // 
            this.mouseYLabel.AutoSize = true;
            this.mouseYLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.mouseYLabel.Location = new System.Drawing.Point(6, 40);
            this.mouseYLabel.Name = "mouseYLabel";
            this.mouseYLabel.Size = new System.Drawing.Size(24, 20);
            this.mouseYLabel.TabIndex = 4;
            this.mouseYLabel.Text = "Y:";
            // 
            // mouseScrollLabel
            // 
            this.mouseScrollLabel.AutoSize = true;
            this.mouseScrollLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.mouseScrollLabel.Location = new System.Drawing.Point(6, 72);
            this.mouseScrollLabel.Name = "mouseScrollLabel";
            this.mouseScrollLabel.Size = new System.Drawing.Size(52, 20);
            this.mouseScrollLabel.TabIndex = 5;
            this.mouseScrollLabel.Text = "Scroll:";
            // 
            // mouseLeftBox
            // 
            this.mouseLeftBox.AutoSize = true;
            this.mouseLeftBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.mouseLeftBox.Location = new System.Drawing.Point(10, 131);
            this.mouseLeftBox.Name = "mouseLeftBox";
            this.mouseLeftBox.Size = new System.Drawing.Size(108, 24);
            this.mouseLeftBox.TabIndex = 6;
            this.mouseLeftBox.Text = "Mouse Left";
            this.mouseLeftBox.UseVisualStyleBackColor = true;
            // 
            // mouseMiddleBox
            // 
            this.mouseMiddleBox.AutoSize = true;
            this.mouseMiddleBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.mouseMiddleBox.Location = new System.Drawing.Point(10, 161);
            this.mouseMiddleBox.Name = "mouseMiddleBox";
            this.mouseMiddleBox.Size = new System.Drawing.Size(126, 24);
            this.mouseMiddleBox.TabIndex = 7;
            this.mouseMiddleBox.Text = "Mouse Middle";
            this.mouseMiddleBox.UseVisualStyleBackColor = true;
            // 
            // mouseRightBox
            // 
            this.mouseRightBox.AutoSize = true;
            this.mouseRightBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.mouseRightBox.Location = new System.Drawing.Point(10, 191);
            this.mouseRightBox.Name = "mouseRightBox";
            this.mouseRightBox.Size = new System.Drawing.Size(118, 24);
            this.mouseRightBox.TabIndex = 8;
            this.mouseRightBox.Text = "Mouse Right";
            this.mouseRightBox.UseVisualStyleBackColor = true;
            // 
            // mouse4Box
            // 
            this.mouse4Box.AutoSize = true;
            this.mouse4Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.mouse4Box.Location = new System.Drawing.Point(10, 221);
            this.mouse4Box.Name = "mouse4Box";
            this.mouse4Box.Size = new System.Drawing.Size(89, 24);
            this.mouse4Box.TabIndex = 9;
            this.mouse4Box.Text = "Mouse 4";
            this.mouse4Box.UseVisualStyleBackColor = true;
            // 
            // mouse5Box
            // 
            this.mouse5Box.AutoSize = true;
            this.mouse5Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.mouse5Box.Location = new System.Drawing.Point(10, 251);
            this.mouse5Box.Name = "mouse5Box";
            this.mouse5Box.Size = new System.Drawing.Size(89, 24);
            this.mouse5Box.TabIndex = 10;
            this.mouse5Box.Text = "Mouse 5";
            this.mouse5Box.UseVisualStyleBackColor = true;
            // 
            // addFrameButton
            // 
            this.addFrameButton.Location = new System.Drawing.Point(12, 386);
            this.addFrameButton.Name = "addFrameButton";
            this.addFrameButton.Size = new System.Drawing.Size(140, 23);
            this.addFrameButton.TabIndex = 3;
            this.addFrameButton.Text = "Add Frame";
            this.addFrameButton.UseVisualStyleBackColor = true;
            this.addFrameButton.Click += new System.EventHandler(this.addFrameButton_Click);
            // 
            // removeFrameButton
            // 
            this.removeFrameButton.Location = new System.Drawing.Point(12, 415);
            this.removeFrameButton.Name = "removeFrameButton";
            this.removeFrameButton.Size = new System.Drawing.Size(140, 23);
            this.removeFrameButton.TabIndex = 4;
            this.removeFrameButton.Text = "Remove Frame";
            this.removeFrameButton.UseVisualStyleBackColor = true;
            this.removeFrameButton.Click += new System.EventHandler(this.removeFrameButton_Click);
            // 
            // InputEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 450);
            this.Controls.Add(this.removeFrameButton);
            this.Controls.Add(this.addFrameButton);
            this.Controls.Add(this.framePageControl);
            this.Controls.Add(this.frameBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "InputEditor";
            this.Text = "Input Editor";
            this.framePageControl.ResumeLayout(false);
            this.general.ResumeLayout(false);
            this.general.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frameLengthInput)).EndInit();
            this.keyboard.ResumeLayout(false);
            this.mouse.ResumeLayout(false);
            this.mouse.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mouseXInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mouseYInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mouseScrollInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox frameBox;
        private System.Windows.Forms.TabControl framePageControl;
        private System.Windows.Forms.TabPage general;
        private System.Windows.Forms.TabPage keyboard;
        private System.Windows.Forms.TabPage mouse;
        private System.Windows.Forms.TabPage gamepad1;
        private System.Windows.Forms.TabPage gamepad2;
        private System.Windows.Forms.TabPage gamepad3;
        private System.Windows.Forms.TabPage gamepad4;
        private System.Windows.Forms.NumericUpDown frameLengthInput;
        private System.Windows.Forms.Label frameLengthLabel;
        private System.Windows.Forms.ListBox keysBox;
        private System.Windows.Forms.Button removeKeyButton;
        private System.Windows.Forms.Button addKeyButton;
        private System.Windows.Forms.NumericUpDown mouseXInput;
        private System.Windows.Forms.NumericUpDown mouseScrollInput;
        private System.Windows.Forms.NumericUpDown mouseYInput;
        private System.Windows.Forms.Label mouseXLabel;
        private System.Windows.Forms.Label mouseScrollLabel;
        private System.Windows.Forms.Label mouseYLabel;
        private System.Windows.Forms.CheckBox mouseLeftBox;
        private System.Windows.Forms.CheckBox mouse5Box;
        private System.Windows.Forms.CheckBox mouse4Box;
        private System.Windows.Forms.CheckBox mouseRightBox;
        private System.Windows.Forms.CheckBox mouseMiddleBox;
        private System.Windows.Forms.Button addFrameButton;
        private System.Windows.Forms.Button removeFrameButton;
    }
}

