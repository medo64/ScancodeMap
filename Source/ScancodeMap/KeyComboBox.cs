using System;
using System.Globalization;
using System.Windows.Forms;

namespace ScancodeMap {
    internal class KeyComboBox : ComboBox {
        public KeyComboBox()
            : base() {
            foreach (var key in ScancodeKey.GetAllKeys()) {
                this.Items.Add(key);
            }
            this.AutoCompleteMode = AutoCompleteMode.Append;
            this.AutoCompleteSource = AutoCompleteSource.ListItems;
        }


        public ScancodeKey SelectedKey {
            get {
                if (this.SelectedItem is ScancodeKey key) {
                    return key;
                } else {
                    var text = this.Text.Trim();
                    if (int.TryParse(text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var scancode)) {
                        return new ScancodeKey(scancode);
                    } else if (text.StartsWith("0x", StringComparison.CurrentCultureIgnoreCase) && int.TryParse(text.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var scancode2)) {
                        return new ScancodeKey(scancode2);
                    } else {
                        return null;
                    }
                }
            }
        }

    }
}
