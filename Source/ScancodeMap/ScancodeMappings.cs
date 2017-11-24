using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Win32;

namespace ScancodeMap {
    internal class ScancodeMappings : IEnumerable<KeyValuePair<ScancodeKey, ScancodeKey>> {

        private static readonly string KeyNameRoot = @"SYSTEM\CurrentControlSet\Control";
        private static readonly string KeyNameSub = @"Keyboard Layout";
        private static readonly string ValueName = "Scancode Map";

        private Dictionary<ScancodeKey, ScancodeKey> ScancodeMap = new Dictionary<ScancodeKey, ScancodeKey>();

        public ScancodeMappings() {
            Reset();
        }


        #region IEnumerable

        public IEnumerator<KeyValuePair<ScancodeKey, ScancodeKey>> GetEnumerator() {
            foreach (var entry in ScancodeMap) {
                yield return entry;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        #endregion IEnumerable


        public void Reset() {
            using (var keyboardLayoutKey = Registry.LocalMachine.OpenSubKey(KeyNameRoot + "\\" + KeyNameSub, writable: false)) {
                if (keyboardLayoutKey != null) {
                    var value = keyboardLayoutKey.GetValue(ValueName);
                    if (value is byte[] bytes) {
                        if (bytes.Length >= 12) {
                            var version = BitConverter.ToInt32(bytes, 0);
                            var flags = BitConverter.ToInt32(bytes, 4);
                            var count = BitConverter.ToInt32(bytes, 8);
                            if (bytes.Length == 12 + count * 4) {
                                for (var i = 12; i < bytes.Length; i += 4) {
                                    var mapping = BitConverter.ToUInt32(bytes, i);
                                    if (mapping != 0) {
                                        var mapFrom = (int)((mapping >> 16) & 0xFFFF);
                                        var mapTo = (int)(mapping & 0xFFFF);
                                        ScancodeMap.Add(new ScancodeKey(mapFrom), new ScancodeKey(mapTo));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Clear() {
            this.ScancodeMap.Clear();
        }

        public void Add(ScancodeKey keyFrom, ScancodeKey keyTo) {
            if (this.ScancodeMap.ContainsKey(keyFrom)) { this.ScancodeMap.Remove(keyFrom); } //remove previous mapping if exists
            this.ScancodeMap.Add(keyFrom, keyTo);
        }

        public void Remove(ScancodeKey key) {
            if (this.ScancodeMap.ContainsKey(key)) {
                this.ScancodeMap.Remove(key);
            }
        }


        public byte[] GetBytes() {
            if (this.ScancodeMap.Count == 0) { return new byte[0]; }

            var count = (this.ScancodeMap.Count + 1);

            var buffer = new byte[12 + count * 4];

            Buffer.BlockCopy(BitConverter.GetBytes(0), 0, buffer, 0, 4); //version
            Buffer.BlockCopy(BitConverter.GetBytes(0), 0, buffer, 4, 4); //flags
            Buffer.BlockCopy(BitConverter.GetBytes(count), 0, buffer, 8, 4); //count

            var i = 12;
            foreach (var entry in this.ScancodeMap) {
                var scancodeFrom = (ushort)(entry.Key.Scancode & 0xFFFF);
                var scancodeTo = (ushort)(entry.Value.Scancode & 0xFFFF);
                var mapping = (scancodeFrom << 16) | scancodeTo;
                Buffer.BlockCopy(BitConverter.GetBytes(mapping), 0, buffer, i, 4);
                i += 4;
            }

            Buffer.BlockCopy(BitConverter.GetBytes(0), 0, buffer, i, 4); //null suffix

            return buffer;
        }

        public string GetBytesAsHex() {
            var sb = new StringBuilder();
            foreach (var b in this.GetBytes()) {
                sb.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);
            }
            return (sb.Length > 0) ? sb.ToString() : "-";
        }

    }
}
