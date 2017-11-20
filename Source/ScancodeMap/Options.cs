using System;
using System.ComponentModel;
using Medo.Configuration;

namespace ScancodeMap {
    internal static class Options {

        [Category("Visual")]
        [DisplayName("Scale boost")]
        [Description("Additional value to determine toolbar scaling.")]
        [DefaultValue(0)]
        public static double ScaleBoost {
            get { return Config.Read("ScaleBoost", 0.00); }
            set {
                if ((value < -1) || (value > 4)) { return; }
                Config.Write("ScaleBoost", value);
            }
        }

    }
}
