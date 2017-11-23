using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace ScancodeMap {
    internal class ScancodeKey {
        public ScancodeKey(int scancode) {
            this.Scancode = scancode;
            this.Text = ScancodeKey.GetText(scancode);
        }

        private ScancodeKey(int scancode, string text) {
            this.Scancode = scancode;
            this.Text = text;
        }


        public int Scancode { get; }
        public string Text { get; }


        public override bool Equals(object obj) {
            if (obj is ScancodeKey key) {
                return (this.Scancode == key.Scancode);
            } else if (obj is int scancode) {
                return (this.Scancode == scancode);
            } else {
                return false;
            }
        }

        public override int GetHashCode() {
            return this.Scancode.GetHashCode();
        }

        public override string ToString() {
            return this.Text;
        }


        #region Static

        private static Dictionary<int, ScancodeKey> ScancodeKeyMapping;
        private static List<ScancodeKey> Keys;

        private static void Init() {
            if (Keys == null) {
                ScancodeKey.Keys = new List<ScancodeKey>();
                ScancodeKey.ScancodeKeyMapping = new Dictionary<int, ScancodeKey>();

                var assembly = Assembly.GetEntryAssembly();
                using (var sr = new StreamReader(assembly.GetManifestResourceStream(assembly.GetName().Name + ".Resources.Mapping.txt"))) {
                    while (!sr.EndOfStream) {
                        var line = sr.ReadLine();
                        if (!string.IsNullOrWhiteSpace(line) && !(line.StartsWith("#", StringComparison.InvariantCulture))) {
                            var parts = line.Split(new char[] { ' ' }, 2);
                            var scancode = int.Parse(parts[0], NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                            var text = parts[1].Trim();
                            var key = new ScancodeKey(scancode, text);
                            Keys.Add(key);
                            ScancodeKeyMapping.Add(scancode, key);
                        }
                    }
                }
            }
        }

        public static string GetText(int scancode) {
            Init();
            if (scancode == 0) {
                return "Disabled";
            } else if (ScancodeKeyMapping.TryGetValue(scancode, out var key)) {
                return key.Text;
            } else {
                return "(unrecognized " + scancode.ToString("X2") + ")";
            }
        }

        public static IEnumerable<ScancodeKey> GetAllKeys() {
            Init();
            return Keys.AsReadOnly();
        }

        #endregion Static

    }
}
