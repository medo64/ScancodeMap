using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace ScancodeMapExec {
    internal static class App {

        [STAThread]
        private static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Medo.Application.UnhandledCatch.ThreadException += new EventHandler<ThreadExceptionEventArgs>(UnhandledCatch_ThreadException);
            Medo.Application.UnhandledCatch.Attach();


            var value = string.Join("", args);
            if (value.Length > 0) {
                if (string.Equals(value, "-")) { //delete mappings

                    try {
                        MapWriter.Clear();
                    } catch (Exception ex) {
                        Medo.MessageBox.ShowError(null, "Cannot clear registry value!\n\n" + ex.Message);
                        Environment.Exit(1);
                    }

                } else if ((value.Length % 2) == 0) { //parse hex

                    var bytes = new List<byte>();
                    for (var i = 0; i < value.Length; i += 2) {
                        if (byte.TryParse(value.Substring(i, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var b)) {
                            bytes.Add(b);
                        } else {
                            Medo.MessageBox.ShowError(null, "Unparseable command-line syntax!");
                            Environment.Exit(1);
                            break;
                        }
                    }

                    try {
                        MapWriter.Write(bytes.ToArray());
                    } catch (Exception ex) {
                        Medo.MessageBox.ShowError(null, "Cannot write to registry value!\n\n" + ex.Message);
                        Environment.Exit(1);
                    }

                } else {
                    Medo.MessageBox.ShowError(null, "Unknown command-line syntax!");
                    Environment.Exit(1);
                }
            } else {
                Medo.MessageBox.ShowError(null, "Unrecognized command-line syntax!");
                Environment.Exit(1);
            }
        }


        private static void UnhandledCatch_ThreadException(object sender, ThreadExceptionEventArgs e) {
#if !DEBUG
            Medo.Diagnostics.ErrorReport.ShowDialog(null, e.Exception, new Uri("https://medo64.com/feedback/"));
#else
            throw e.Exception;
#endif
        }
    }
}
