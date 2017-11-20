namespace ScancodeMap {
    partial class EditForm {
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
            this.components = new System.ComponentModel.Container();
            this.cmbScancodeFrom = new ScancodeMap.KeyComboBox();
            this.btnScancodeFrom = new System.Windows.Forms.Button();
            this.btnScancodeTo = new System.Windows.Forms.Button();
            this.lblScancodeFrom = new System.Windows.Forms.Label();
            this.lblScancodeTo = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tip = new System.Windows.Forms.ToolTip(this.components);
            this.cmbScancodeTo = new ScancodeMap.KeyComboBox();
            this.SuspendLayout();
            // 
            // cmbScancodeFrom
            // 
            this.cmbScancodeFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbScancodeFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbScancodeFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbScancodeFrom.FormattingEnabled = true;
            this.cmbScancodeFrom.Location = new System.Drawing.Point(66, 12);
            this.cmbScancodeFrom.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.cmbScancodeFrom.Name = "cmbScancodeFrom";
            this.cmbScancodeFrom.Size = new System.Drawing.Size(180, 24);
            this.cmbScancodeFrom.TabIndex = 1;
            this.cmbScancodeFrom.SelectedValueChanged += new System.EventHandler(this.cmbScancode_SelectedValueChanged);
            // 
            // btnScancodeFrom
            // 
            this.btnScancodeFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScancodeFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScancodeFrom.Location = new System.Drawing.Point(246, 12);
            this.btnScancodeFrom.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.btnScancodeFrom.Name = "btnScancodeFrom";
            this.btnScancodeFrom.Size = new System.Drawing.Size(24, 24);
            this.btnScancodeFrom.TabIndex = 2;
            this.btnScancodeFrom.Text = "...";
            this.tip.SetToolTip(this.btnScancodeFrom, "If activated, the next pressed key will be automatically selected.");
            this.btnScancodeFrom.UseVisualStyleBackColor = true;
            this.btnScancodeFrom.Click += new System.EventHandler(this.btnScancodeFrom_Click);
            // 
            // btnScancodeTo
            // 
            this.btnScancodeTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScancodeTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScancodeTo.Location = new System.Drawing.Point(246, 42);
            this.btnScancodeTo.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.btnScancodeTo.Name = "btnScancodeTo";
            this.btnScancodeTo.Size = new System.Drawing.Size(24, 24);
            this.btnScancodeTo.TabIndex = 5;
            this.btnScancodeTo.Text = "...";
            this.tip.SetToolTip(this.btnScancodeTo, "If activated, the next pressed key will be automatically selected.");
            this.btnScancodeTo.UseVisualStyleBackColor = true;
            this.btnScancodeTo.Click += new System.EventHandler(this.btnScancodeTo_Click);
            // 
            // lblScancodeFrom
            // 
            this.lblScancodeFrom.AutoSize = true;
            this.lblScancodeFrom.Location = new System.Drawing.Point(12, 15);
            this.lblScancodeFrom.Name = "lblScancodeFrom";
            this.lblScancodeFrom.Size = new System.Drawing.Size(44, 17);
            this.lblScancodeFrom.TabIndex = 0;
            this.lblScancodeFrom.Text = "From:";
            // 
            // lblScancodeTo
            // 
            this.lblScancodeTo.AutoSize = true;
            this.lblScancodeTo.Location = new System.Drawing.Point(12, 45);
            this.lblScancodeTo.Name = "lblScancodeTo";
            this.lblScancodeTo.Size = new System.Drawing.Size(29, 17);
            this.lblScancodeTo.TabIndex = 3;
            this.lblScancodeTo.Text = "To:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(180, 84);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 26);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(84, 84);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 26);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cmbScancodeTo
            // 
            this.cmbScancodeTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbScancodeTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbScancodeTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbScancodeTo.FormattingEnabled = true;
            this.cmbScancodeTo.Location = new System.Drawing.Point(66, 42);
            this.cmbScancodeTo.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.cmbScancodeTo.Name = "cmbScancodeTo";
            this.cmbScancodeTo.Size = new System.Drawing.Size(180, 24);
            this.cmbScancodeTo.TabIndex = 4;
            this.cmbScancodeTo.SelectedValueChanged += new System.EventHandler(this.cmbScancode_SelectedValueChanged);
            // 
            // EditForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(282, 122);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblScancodeTo);
            this.Controls.Add(this.lblScancodeFrom);
            this.Controls.Add(this.btnScancodeTo);
            this.Controls.Add(this.cmbScancodeTo);
            this.Controls.Add(this.btnScancodeFrom);
            this.Controls.Add(this.cmbScancodeFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit";
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KeyComboBox cmbScancodeFrom;
        private System.Windows.Forms.Button btnScancodeFrom;
        private System.Windows.Forms.Button btnScancodeTo;
        private System.Windows.Forms.Label lblScancodeFrom;
        private System.Windows.Forms.Label lblScancodeTo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ToolTip tip;
        private KeyComboBox cmbScancodeTo;
    }
}