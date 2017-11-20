using System;
using System.Drawing;
using System.Windows.Forms;
using ScancodeMap;

namespace ScancodeView {
    internal partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
            Medo.Windows.Forms.State.SetupOnLoadAndClose(this);

            App.Hook.KeyboardCallback += this.Hook_KeyboardCallback;
        }


        private void Form_Activated(object sender, EventArgs e) {
            App.Hook.Hook();

            foreach (var control in new Control[] { txtKey, txtScancodeHex, txtScancodeDec, txtVirtualKeyCodeHex, txtVirtualKeyCodeDec }) {
                control.BackColor = SystemColors.Window;
            }
        }

        private void Form_Deactivate(object sender, EventArgs e) {
            App.Hook.Unhook();

            foreach (var control in new Control[] { txtKey, txtScancodeHex, txtScancodeDec, txtVirtualKeyCodeHex, txtVirtualKeyCodeDec }) {
                control.BackColor = SystemColors.Control;
            }
        }


        private void Hook_KeyboardCallback(object sender, Medo.Win32.LowLevelKeyboardHookCallbackEventArgs e) {
            if (e.IsPressed == false) {
                var scanCode = (e.IsExtended ? 0xE000 : 0) | e.ScanCode;

                txtKey.Text = ScancodeKey.GetText(scanCode);

                txtScancodeHex.Text = "0x" + (e.IsExtended ? scanCode.ToString("X4") : scanCode.ToString("X2"));
                txtScancodeDec.Text = scanCode.ToString();

                txtVirtualKeyCodeHex.Text = "0x" + e.VirtualKeyCode.ToString("X2");
                txtVirtualKeyCodeDec.Text = e.VirtualKeyCode.ToString();
            }
        }
    }
}
