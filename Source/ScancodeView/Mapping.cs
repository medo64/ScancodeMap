using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace ScancodeView {
    internal static class Mapping {

        private static Dictionary<int, string> ScancodeTextMapping;

        public static string GetText(int scanCode) {
            if (ScancodeTextMapping == null) {
                ScancodeTextMapping = new Dictionary<int, string>();
                using (var sr = new StreamReader(Assembly.GetEntryAssembly().GetManifestResourceStream("ScancodeView.Resources.Mapping.txt"))) {
                    while (!sr.EndOfStream) {
                        var line = sr.ReadLine();
                        if (!string.IsNullOrWhiteSpace(line)) {
                            var parts = line.Split(new char[] { ' ' }, 2);
                            var key = int.Parse(parts[0], NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                            var value = parts[1].Trim();
                            ScancodeTextMapping.Add(key, value);
                        }
                    }
                }
            }
            if (ScancodeTextMapping.TryGetValue(scanCode, out var text)) {
                return text;
            } else {
                return "(unrecognized)";
            }
        }

    }
}
