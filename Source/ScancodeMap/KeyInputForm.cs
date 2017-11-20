using System;
using System.Drawing;
using System.Windows.Forms;
using Medo.Win32;

namespace ScancodeMap {
    internal partial class KeyInputForm : Form {
        public KeyInputForm() {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
        }


        private LowLevelKeyboardHook Keyboard = new LowLevelKeyboardHook();


        private void Form_Load(object sender, EventArgs e) {
            this.Keyboard.Hook();
            this.Keyboard.KeyboardCallback += this.Keyboard_KeyboardCallback;
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e) {
            this.Keyboard.Unhook();
        }


        private void Keyboard_KeyboardCallback(object sender, LowLevelKeyboardHookCallbackEventArgs e) {
            if (e.IsPressed == false) {
                var scanCode = (e.IsExtended ? 0xE000 : 0) | e.ScanCode;
                this.SelectedKey = new ScancodeKey(scanCode);
                this.DialogResult = DialogResult.OK;
            }
        }


        public ScancodeKey SelectedKey { get; private set; }

    }
}
