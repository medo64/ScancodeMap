using System;
using Microsoft.Win32;

namespace ScancodeMapExec {
    internal static class MapWriter {

        private static readonly string KeyNameRoot = @"SYSTEM\CurrentControlSet\Control";
        private static readonly string KeyNameSub = @"Keyboard Layout";
        private static readonly string ValueName = "Scancode Map";

        public static void Clear() {
            using (var keyboardLayoutKey = Registry.LocalMachine.OpenSubKey(KeyNameRoot + "\\" + KeyNameSub, writable: true)) {
                if (keyboardLayoutKey == null) { return; } //since key is not here, we're good

                var value = keyboardLayoutKey.GetValue(ValueName);
                if (value == null) { return; } //since value is not here, we're good

                keyboardLayoutKey.DeleteValue(ValueName);
            }
        }

        public static void Write(byte[] bytes) {
            using (var rootKey = Registry.LocalMachine.OpenSubKey(KeyNameRoot, writable: true)) {
                if (rootKey == null) { throw new InvalidOperationException("Cannot open registry key."); }

                using (var subKey = rootKey.OpenSubKey(KeyNameSub, writable: true)) {
                    RegistryKey key = null;
                    try {
                        key = subKey ?? rootKey.CreateSubKey(KeyNameSub);
                        if ((key.GetValue(ValueName) != null) && (key.GetValueKind(ValueName) != RegistryValueKind.Binary)) { //if wrong type, delete it first
                            key.DeleteValue(ValueName);
                        }
                        key.SetValue(ValueName, bytes, RegistryValueKind.Binary);
                    } finally {
                        if ((subKey == null) && (key != null)) { key.Close(); }
                    }
                }
            }
        }

    }
}
