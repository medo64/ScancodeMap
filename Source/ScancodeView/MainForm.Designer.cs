namespace ScancodeView {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblKey = new System.Windows.Forms.Label();
            this.lblScancode = new System.Windows.Forms.Label();
            this.lblVirtualKeyCode = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.Label();
            this.txtScancodeHex = new System.Windows.Forms.Label();
            this.txtScancodeDec = new System.Windows.Forms.Label();
            this.txtVirtualKeyCodeHex = new System.Windows.Forms.Label();
            this.txtVirtualKeyCodeDec = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(12, 12);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(36, 17);
            this.lblKey.TabIndex = 0;
            this.lblKey.Text = "Key:";
            // 
            // lblScancode
            // 
            this.lblScancode.AutoSize = true;
            this.lblScancode.Location = new System.Drawing.Point(12, 43);
            this.lblScancode.Name = "lblScancode";
            this.lblScancode.Size = new System.Drawing.Size(79, 17);
            this.lblScancode.TabIndex = 2;
            this.lblScancode.Text = "Scan code:";
            // 
            // lblVirtualKeyCode
            // 
            this.lblVirtualKeyCode.AutoSize = true;
            this.lblVirtualKeyCode.Location = new System.Drawing.Point(12, 71);
            this.lblVirtualKeyCode.Name = "lblVirtualKeyCode";
            this.lblVirtualKeyCode.Size = new System.Drawing.Size(113, 17);
            this.lblVirtualKeyCode.TabIndex = 5;
            this.lblVirtualKeyCode.Text = "Virtual key code:";
            // 
            // txtKey
            // 
            this.txtKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKey.BackColor = System.Drawing.SystemColors.Window;
            this.txtKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKey.Location = new System.Drawing.Point(140, 12);
            this.txtKey.Margin = new System.Windows.Forms.Padding(3);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(180, 22);
            this.txtKey.TabIndex = 1;
            this.txtKey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtScancodeHex
            // 
            this.txtScancodeHex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScancodeHex.BackColor = System.Drawing.SystemColors.Window;
            this.txtScancodeHex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtScancodeHex.Location = new System.Drawing.Point(140, 40);
            this.txtScancodeHex.Margin = new System.Windows.Forms.Padding(3);
            this.txtScancodeHex.Name = "txtScancodeHex";
            this.txtScancodeHex.Size = new System.Drawing.Size(87, 22);
            this.txtScancodeHex.TabIndex = 3;
            this.txtScancodeHex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtScancodeDec
            // 
            this.txtScancodeDec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScancodeDec.BackColor = System.Drawing.SystemColors.Window;
            this.txtScancodeDec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtScancodeDec.Location = new System.Drawing.Point(233, 40);
            this.txtScancodeDec.Margin = new System.Windows.Forms.Padding(3);
            this.txtScancodeDec.Name = "txtScancodeDec";
            this.txtScancodeDec.Size = new System.Drawing.Size(87, 22);
            this.txtScancodeDec.TabIndex = 4;
            this.txtScancodeDec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtVirtualKeyCodeHex
            // 
            this.txtVirtualKeyCodeHex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVirtualKeyCodeHex.BackColor = System.Drawing.SystemColors.Window;
            this.txtVirtualKeyCodeHex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVirtualKeyCodeHex.Location = new System.Drawing.Point(140, 68);
            this.txtVirtualKeyCodeHex.Margin = new System.Windows.Forms.Padding(3);
            this.txtVirtualKeyCodeHex.Name = "txtVirtualKeyCodeHex";
            this.txtVirtualKeyCodeHex.Size = new System.Drawing.Size(87, 22);
            this.txtVirtualKeyCodeHex.TabIndex = 6;
            this.txtVirtualKeyCodeHex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtVirtualKeyCodeDec
            // 
            this.txtVirtualKeyCodeDec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVirtualKeyCodeDec.BackColor = System.Drawing.SystemColors.Window;
            this.txtVirtualKeyCodeDec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVirtualKeyCodeDec.Location = new System.Drawing.Point(233, 68);
            this.txtVirtualKeyCodeDec.Margin = new System.Windows.Forms.Padding(3);
            this.txtVirtualKeyCodeDec.Name = "txtVirtualKeyCodeDec";
            this.txtVirtualKeyCodeDec.Size = new System.Drawing.Size(87, 22);
            this.txtVirtualKeyCodeDec.TabIndex = 7;
            this.txtVirtualKeyCodeDec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 102);
            this.Controls.Add(this.txtVirtualKeyCodeDec);
            this.Controls.Add(this.txtVirtualKeyCodeHex);
            this.Controls.Add(this.txtScancodeDec);
            this.Controls.Add(this.txtScancodeHex);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.lblVirtualKeyCode);
            this.Controls.Add(this.lblScancode);
            this.Controls.Add(this.lblKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Scancode Viewer";
            this.Activated += new System.EventHandler(this.Form_Activated);
            this.Deactivate += new System.EventHandler(this.Form_Deactivate);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.Label lblScancode;
        private System.Windows.Forms.Label lblVirtualKeyCode;
        private System.Windows.Forms.Label txtKey;
        private System.Windows.Forms.Label txtScancodeHex;
        private System.Windows.Forms.Label txtScancodeDec;
        private System.Windows.Forms.Label txtVirtualKeyCodeHex;
        private System.Windows.Forms.Label txtVirtualKeyCodeDec;
    }
}

