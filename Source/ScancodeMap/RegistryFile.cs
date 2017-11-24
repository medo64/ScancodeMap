using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace ScancodeMap {
    internal static class RegistryFile {

        public static void Export(string fileName, byte[] bytes) {
            var sb = new StringBuilder();
            if (bytes.Length > 0) {
                sb.Append(@"""Scancode Map""=hex:");
                for (var i = 0; i < bytes.Length; i++) {
                    if (i > 0) { sb.Append(","); }
                    sb.Append(bytes[i].ToString("x2", CultureInfo.InvariantCulture));
                }
            } else {
                sb.Append(@"""Scancode Map""=-");
            }

            using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(file, Encoding.Unicode)) {
                writer.WriteLine(@"Windows Registry Editor Version 5.00");
                writer.WriteLine();
                writer.WriteLine(@"[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Keyboard Layout]");
                writer.WriteLine(sb.ToString());
            }
        }

    }
}
