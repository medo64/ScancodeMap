namespace ScancodeMap {
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
            this.lsvMapping = new System.Windows.Forms.ListView();
            this.lsvMapping_colFrom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lsvMapping_colTo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnu = new System.Windows.Forms.ToolStrip();
            this.mnuNew = new System.Windows.Forms.ToolStripButton();
            this.mnuApp = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuAppFeedback = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAppUpgrade = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAppAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAdd = new System.Windows.Forms.ToolStripButton();
            this.mnuEdit = new System.Windows.Forms.ToolStripButton();
            this.mnuRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuApply = new System.Windows.Forms.ToolStripButton();
            this.bwUpgradeCheck = new System.ComponentModel.BackgroundWorker();
            this.mnu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsvMapping
            // 
            this.lsvMapping.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsvMapping_colFrom,
            this.lsvMapping_colTo});
            this.lsvMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvMapping.FullRowSelect = true;
            this.lsvMapping.GridLines = true;
            this.lsvMapping.HideSelection = false;
            this.lsvMapping.Location = new System.Drawing.Point(0, 27);
            this.lsvMapping.MultiSelect = false;
            this.lsvMapping.Name = "lsvMapping";
            this.lsvMapping.ShowItemToolTips = true;
            this.lsvMapping.Size = new System.Drawing.Size(382, 326);
            this.lsvMapping.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lsvMapping.TabIndex = 0;
            this.lsvMapping.UseCompatibleStateImageBehavior = false;
            this.lsvMapping.View = System.Windows.Forms.View.Details;
            this.lsvMapping.ItemActivate += new System.EventHandler(this.lsvMapping_ItemActivate);
            this.lsvMapping.SelectedIndexChanged += new System.EventHandler(this.lsvMapping_SelectedIndexChanged);
            // 
            // lsvMapping_colFrom
            // 
            this.lsvMapping_colFrom.Text = "From";
            this.lsvMapping_colFrom.Width = 150;
            // 
            // lsvMapping_colTo
            // 
            this.lsvMapping_colTo.Text = "To";
            this.lsvMapping_colTo.Width = 150;
            // 
            // mnu
            // 
            this.mnu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNew,
            this.mnuApp,
            this.mnuExport,
            this.toolStripSeparator1,
            this.mnuAdd,
            this.mnuEdit,
            this.mnuRemove,
            this.toolStripSeparator2,
            this.mnuApply});
            this.mnu.Location = new System.Drawing.Point(0, 0);
            this.mnu.Name = "mnu";
            this.mnu.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.mnu.Size = new System.Drawing.Size(382, 27);
            this.mnu.TabIndex = 1;
            // 
            // mnuNew
            // 
            this.mnuNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuNew.Image = global::ScancodeMap.Properties.Resources.mnuNew_16;
            this.mnuNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuNew.Name = "mnuNew";
            this.mnuNew.Size = new System.Drawing.Size(24, 24);
            this.mnuNew.Text = "&New";
            this.mnuNew.ToolTipText = "Clean mappings (Ctrl+N)";
            this.mnuNew.Click += new System.EventHandler(this.mnuNew_Click);
            // 
            // mnuApp
            // 
            this.mnuApp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnuApp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuApp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAppFeedback,
            this.mnuAppUpgrade,
            this.toolStripMenuItem2,
            this.mnuAppAbout});
            this.mnuApp.Image = global::ScancodeMap.Properties.Resources.mnuApp_16;
            this.mnuApp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuApp.Name = "mnuApp";
            this.mnuApp.Size = new System.Drawing.Size(34, 24);
            this.mnuApp.Text = "Application";
            this.mnuApp.ToolTipText = "Application menu";
            // 
            // mnuAppFeedback
            // 
            this.mnuAppFeedback.Name = "mnuAppFeedback";
            this.mnuAppFeedback.Size = new System.Drawing.Size(206, 26);
            this.mnuAppFeedback.Text = "Send &feedback";
            this.mnuAppFeedback.Click += new System.EventHandler(this.mnuAppFeedback_Click);
            // 
            // mnuAppUpgrade
            // 
            this.mnuAppUpgrade.Name = "mnuAppUpgrade";
            this.mnuAppUpgrade.Size = new System.Drawing.Size(206, 26);
            this.mnuAppUpgrade.Text = "Check for &upgrade";
            this.mnuAppUpgrade.Click += new System.EventHandler(this.mnuAppUpgrade_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(203, 6);
            // 
            // mnuAppAbout
            // 
            this.mnuAppAbout.Name = "mnuAppAbout";
            this.mnuAppAbout.Size = new System.Drawing.Size(206, 26);
            this.mnuAppAbout.Text = "&About";
            this.mnuAppAbout.Click += new System.EventHandler(this.mnuAppAbout_Click);
            // 
            // mnuExport
            // 
            this.mnuExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuExport.Image = global::ScancodeMap.Properties.Resources.mnuExport_16;
            this.mnuExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuExport.Name = "mnuExport";
            this.mnuExport.Size = new System.Drawing.Size(24, 24);
            this.mnuExport.Text = "Export";
            this.mnuExport.ToolTipText = "Export mappings as registry file.";
            this.mnuExport.Click += new System.EventHandler(this.mnuExport_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // mnuAdd
            // 
            this.mnuAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuAdd.Image = global::ScancodeMap.Properties.Resources.mnuAdd_16;
            this.mnuAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuAdd.Name = "mnuAdd";
            this.mnuAdd.Size = new System.Drawing.Size(24, 24);
            this.mnuAdd.Text = "Add";
            this.mnuAdd.ToolTipText = "Add mapping (Ins)";
            this.mnuAdd.Click += new System.EventHandler(this.mnuAdd_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuEdit.Enabled = false;
            this.mnuEdit.Image = global::ScancodeMap.Properties.Resources.mnuEdit_16;
            this.mnuEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(24, 24);
            this.mnuEdit.Text = "Edit";
            this.mnuEdit.ToolTipText = "Edit mapping (F2)";
            this.mnuEdit.Click += new System.EventHandler(this.mnuEdit_Click);
            // 
            // mnuRemove
            // 
            this.mnuRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuRemove.Enabled = false;
            this.mnuRemove.Image = global::ScancodeMap.Properties.Resources.mnuRemove_16;
            this.mnuRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuRemove.Name = "mnuRemove";
            this.mnuRemove.Size = new System.Drawing.Size(24, 24);
            this.mnuRemove.Text = "Remove";
            this.mnuRemove.ToolTipText = "Remove mapping (Del)";
            this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // mnuApply
            // 
            this.mnuApply.Image = global::ScancodeMap.Properties.Resources.mnuApply_16;
            this.mnuApply.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuApply.Name = "mnuApply";
            this.mnuApply.Size = new System.Drawing.Size(72, 24);
            this.mnuApply.Text = "Apply";
            this.mnuApply.ToolTipText = "Apply mappings (F5)";
            this.mnuApply.Click += new System.EventHandler(this.mnuApply_Click);
            // 
            // bwUpgradeCheck
            // 
            this.bwUpgradeCheck.WorkerSupportsCancellation = true;
            this.bwUpgradeCheck.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwUpgradeCheck_DoWork);
            this.bwUpgradeCheck.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwUpgradeCheck_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 353);
            this.Controls.Add(this.lsvMapping);
            this.Controls.Add(this.mnu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(320, 200);
            this.Name = "MainForm";
            this.Text = "Scancode Map";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FormClosing);
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.mnu.ResumeLayout(false);
            this.mnu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lsvMapping;
        private System.Windows.Forms.ColumnHeader lsvMapping_colFrom;
        private System.Windows.Forms.ColumnHeader lsvMapping_colTo;
        private System.Windows.Forms.ToolStrip mnu;
        private System.Windows.Forms.ToolStripButton mnuNew;
        private System.Windows.Forms.ToolStripDropDownButton mnuApp;
        private System.Windows.Forms.ToolStripMenuItem mnuAppFeedback;
        private System.Windows.Forms.ToolStripMenuItem mnuAppUpgrade;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuAppAbout;
        private System.ComponentModel.BackgroundWorker bwUpgradeCheck;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton mnuAdd;
        private System.Windows.Forms.ToolStripButton mnuEdit;
        private System.Windows.Forms.ToolStripButton mnuRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton mnuApply;
        private System.Windows.Forms.ToolStripButton mnuExport;
    }
}

