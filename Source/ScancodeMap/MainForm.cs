using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Medo.Configuration;

namespace ScancodeMap {
    internal partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
            mnu.Font = SystemFonts.MessageBoxFont;
            lsvMapping.Font = SystemFonts.MessageBoxFont;

            mnu.Renderer = Helpers.ToolStripBorderlessSystemRendererInstance;
            Helpers.ScaleToolstrip(mnu);

            Medo.Windows.Forms.State.SetupOnLoadAndClose(this);

            this.Mappings = new ScancodeMappings();
            UpdateMappings();
        }


        private void Form_Shown(object sender, EventArgs e) {
            if (!Config.IsAssumedInstalled) {
                mnuAppUpgrade.Visible = false; //don't show update for portable executable
            } else {
                var version = Assembly.GetExecutingAssembly().GetName().Version; //don't auto-check for development builds
                if ((version.Major != 0) || (version.Minor != 0)) { bwUpgradeCheck.RunWorkerAsync(); }
            }
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e) {
            bwUpgradeCheck.CancelAsync();
        }


        private void lsvMapping_ItemActivate(object sender, EventArgs e) {
            mnuEdit.PerformClick();
        }

        private void lsvMapping_SelectedIndexChanged(object sender, EventArgs e) {
            mnuEdit.Enabled = (lsvMapping.SelectedIndices.Count == 1);
            mnuRemove.Enabled = (lsvMapping.SelectedIndices.Count > 0);
        }


        #region Menu

        #region MenuKeys

        private bool SuppressMenuKey = false;

        protected override bool ProcessDialogKey(Keys keyData) {
            if (((keyData & Keys.Alt) == Keys.Alt) && (keyData != (Keys.Alt | Keys.Menu))) { this.SuppressMenuKey = true; }

            switch (keyData) {

                case Keys.F10:
                    ToggleMenu();
                    return true;

                case Keys.Control | Keys.N:
                case Keys.Alt | Keys.N:
                    mnuNew.PerformClick();
                    return true;

                case Keys.Insert:
                    mnuAdd.PerformClick();
                    return true;

                case Keys.F2:
                    mnuEdit.PerformClick();
                    return true;

                case Keys.Delete:
                    mnuRemove.PerformClick();
                    return true;

                case Keys.F5:
                    mnuApply.PerformClick();
                    return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            if (e.KeyData == Keys.Menu) {
                if (this.SuppressMenuKey) { this.SuppressMenuKey = false; return; }
                ToggleMenu();
                e.Handled = true;
                e.SuppressKeyPress = true;
            } else {
                base.OnKeyDown(e);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e) {
            if (e.KeyData == Keys.Menu) {
                if (this.SuppressMenuKey) { this.SuppressMenuKey = false; return; }
                ToggleMenu();
                e.Handled = true;
                e.SuppressKeyPress = true;
            } else {
                base.OnKeyUp(e);
            }
        }


        private void ToggleMenu() {
            if (mnu.ContainsFocus) {
                lsvMapping.Select();
            } else {
                mnu.Select();
                mnu.Items[0].Select();
            }
        }

        #endregion MenuKeys


        private void mnuNew_Click(object sender, EventArgs e) {
            this.Mappings.Clear();
            UpdateMappings();
        }


        private void mnuAdd_Click(object sender, EventArgs e) {
            using (var frm = new EditForm()) {
                if (frm.ShowDialog(this) == DialogResult.OK) {
                    Mappings.Add(frm.SelectedMapping.Key, frm.SelectedMapping.Value);
                    UpdateMappings();
                }
            }
        }

        private void mnuEdit_Click(object sender, EventArgs e) {
            if (lsvMapping.SelectedItems.Count != 1) { return; }

            if (lsvMapping.SelectedItems[0].Tag is KeyValuePair<ScancodeKey, ScancodeKey> entry) {
                using (var frm = new EditForm(entry)) {
                    if (frm.ShowDialog(this) == DialogResult.OK) {
                        Mappings.Remove(entry.Key);
                        Mappings.Add(frm.SelectedMapping.Key, frm.SelectedMapping.Value);
                        UpdateMappings();
                    }
                }
            }
        }

        private void mnuRemove_Click(object sender, EventArgs e) {
            if (lsvMapping.SelectedItems.Count == 0) { return; }

            for (var i = lsvMapping.SelectedItems.Count - 1; i >= 0; i--) {
                if (lsvMapping.SelectedItems[i].Tag is KeyValuePair<ScancodeKey, ScancodeKey> entry) {
                    this.Mappings.Remove(entry.Key);
                }
            }

            UpdateMappings();
        }


        private void mnuApply_Click(object sender, EventArgs e) {
            mnuApply.Enabled = false;
            try {
                var startInfo = new ProcessStartInfo("ScancodeMapExec.exe", this.Mappings.GetBytesAsHex());
                var process = new Process() { StartInfo = startInfo };
                try {
                    process.Start();
                    process.WaitForExit();
                    Medo.MessageBox.ShowInformation(this, "Key mappings saved.\nPlease restart machine to activate them.");
                } catch (Win32Exception ex) {
                    Medo.MessageBox.ShowError(this, "Cannot write to registry!\n\n" + ex.Message);
                }
            } finally {
                mnuApply.Enabled = true;
            }
        }


        private void mnuAppFeedback_Click(object sender, EventArgs e) {
            Medo.Diagnostics.ErrorReport.ShowDialog(this, null, new Uri("https://medo64.com/feedback/"));
        }

        private void mnuAppUpgrade_Click(object sender, EventArgs e) {
            Medo.Services.Upgrade.ShowDialog(this, new Uri("https://medo64.com/upgrade/"));
        }

        private void mnuAppAbout_Click(object sender, EventArgs e) {
            Medo.Windows.Forms.AboutBox.ShowDialog(this, new Uri("https://www.medo64.com/scancodemap/"));
        }

        #endregion Menu


        #region Upgrade

        private void bwUpgradeCheck_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
            e.Cancel = true;

            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < 3000) { //wait for three seconds
                Thread.Sleep(100);
                if (bwUpgradeCheck.CancellationPending) { return; }
            }

            var file = Medo.Services.Upgrade.GetUpgradeFile(new Uri("https://medo64.com/upgrade/"));
            if (file != null) {
                if (bwUpgradeCheck.CancellationPending) { return; }
                e.Cancel = false;
            }
        }

        private void bwUpgradeCheck_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) {
            if (!e.Cancelled) {
                Helpers.ScaleToolstripItem(mnuApp, "mnuAppUpgrade");
                mnuAppUpgrade.Text = "Upgrade is available";
            }
        }

        #endregion Upgrade


        #region Mappings

        private ScancodeMappings Mappings;

        private void UpdateMappings() {
            lsvMapping.BeginUpdate();
            lsvMapping.Items.Clear();
            foreach (var entry in this.Mappings) {
                var textFrom = entry.Key.Text;
                var textTo = entry.Value.Text;
                lsvMapping.Items.Add(new ListViewItem(new string[] { textFrom, textTo }) { Tag = entry });
            }
            lsvMapping.EndUpdate();

            lsvMapping_SelectedIndexChanged(null, null);
        }

        #endregion Mappings

    }
}
