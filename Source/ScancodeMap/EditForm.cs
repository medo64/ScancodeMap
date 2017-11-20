using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ScancodeMap {
    internal partial class EditForm : Form {
        public EditForm() {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
            btnScancodeFrom.Font = new Font(this.Font.FontFamily, this.Font.SizeInPoints * 0.75F);
            btnScancodeTo.Font = new Font(this.Font.FontFamily, this.Font.SizeInPoints * 0.75F);

            this.Text = "New";
        }

        public EditForm(KeyValuePair<ScancodeKey, ScancodeKey> entry)
            : this() {
            this.Text = "Edit";

            cmbScancodeFrom.SelectedItem = entry.Key;
            cmbScancodeTo.SelectedItem = entry.Value;
        }


        private void Form_Shown(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(cmbScancodeFrom.Text)) {
                cmbScancodeFrom.Focus();
            } else {
                cmbScancodeTo.Focus();
            }

        }


        private void cmbScancode_SelectedValueChanged(object sender, EventArgs e) {
            btnOK.Enabled = (cmbScancodeFrom.SelectedKey != null)
                         && (cmbScancodeTo.SelectedKey != null)
                         && !(cmbScancodeFrom.SelectedKey.Equals (cmbScancodeTo.SelectedKey));
        }

        private void btnScancodeFrom_Click(object sender, EventArgs e) {
            using (var frm = new KeyInputForm()) {
                if (frm.ShowDialog(this) == DialogResult.OK) {
                    if (!cmbScancodeFrom.Items.Contains(frm.SelectedKey)) {
                        cmbScancodeFrom.Items.Add(frm.SelectedKey); //add if not present already
                    }
                    cmbScancodeFrom.SelectedItem = frm.SelectedKey;
                    cmbScancode_SelectedValueChanged(null, null);
                }
            }
        }

        private void btnScancodeTo_Click(object sender, EventArgs e) {
            using (var frm = new KeyInputForm()) {
                if (frm.ShowDialog(this) == DialogResult.OK) {
                    if (!cmbScancodeTo.Items.Contains(frm.SelectedKey)) {
                        cmbScancodeTo.Items.Add(frm.SelectedKey); //add if not present already
                    }
                    cmbScancodeTo.SelectedItem = frm.SelectedKey;
                    cmbScancode_SelectedValueChanged(null, null);
                }
            }
        }


        public KeyValuePair<ScancodeKey, ScancodeKey> SelectedMapping { get; set; }


        private void btnOK_Click(object sender, EventArgs e) {
            var key = cmbScancodeFrom.SelectedKey;
            var value = cmbScancodeTo.SelectedKey;
            this.SelectedMapping = new KeyValuePair<ScancodeKey, ScancodeKey>(key, value);
        }
    }
}
