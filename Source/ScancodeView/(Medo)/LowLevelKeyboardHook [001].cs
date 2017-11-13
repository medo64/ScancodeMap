//Josip Medved <jmedved@jmedved.com>   www.medo64.com

//2017-11-12: Initial version.


using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Medo.Win32 {
    /// <summary>
    /// Handling low-level keyboard hook.
    /// </summary>
    public class LowLevelKeyboardHook : IDisposable {

        /// <summary>
        /// Create a new instance.
        /// </summary>
        public LowLevelKeyboardHook() {
        }


        /// <summary>
        /// Starts the keyboard hook.
        /// </summary>
        /// <exception cref="Win32Exception"></exception>
        public void Hook() {
            if (this.HookHandle.IsInvalid || this.HookHandle.IsClosed) {
                this.HookHandle = NativeMethods.SetWindowsHookEx(
                    idHook: NativeMethods.WH_KEYBOARD_LL,
                    lpfn: new NativeMethods.LowLevelKeyboardProc(this.KeyboardHookProc),
                    hMod: IntPtr.Zero,
                    dwThreadId: 0);

                if (this.HookHandle.IsInvalid) { throw new Win32Exception(); }
            }
        }

        /// <summary>
        /// Releases the keyboard hook.
        /// </summary>
        public void Unhook() {
            if ((this.HookHandle.IsInvalid == false) && (this.HookHandle.IsClosed == false)) {
                this.HookHandle.Close();
            }
        }


        /// <summary>
        /// Event called when hook receives a callback.
        /// </summary>
        public event EventHandler<LowLevelKeyboardHookCallbackEventArgs> KeyboardCallback;

        private void OnKeyboardCallback(LowLevelKeyboardHookCallbackEventArgs e) {
            this.KeyboardCallback?.Invoke(this, e);
        }


        private NativeMethods.WindowsHookSafeHandle HookHandle = new NativeMethods.WindowsHookSafeHandle();

        private IntPtr KeyboardHookProc(Int32 nCode, IntPtr wParam, IntPtr lParam) {
            var keyboardHookStruct = (NativeMethods.KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(NativeMethods.KBDLLHOOKSTRUCT));

            if (nCode == NativeMethods.HC_ACTION) {
                OnKeyboardCallback(new LowLevelKeyboardHookCallbackEventArgs(
                    virtualKeyCode: keyboardHookStruct.vkCode,
                    scanCode: keyboardHookStruct.scanCode,
                    flags: keyboardHookStruct.flags));
                return NativeMethods.CallNextHookEx(HookHandle, nCode, wParam, lParam);
            } else {
                return NativeMethods.CallNextHookEx(HookHandle, nCode, wParam, lParam);
            }
        }


        #region IDispose

        /// <summary>
        /// Destroys the instance.
        /// </summary>
        ~LowLevelKeyboardHook() {
            this.Dispose(false);
        }

        /// <summary>
        /// Disposes resources in use.
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes resources in use.
        /// </summary>
        protected virtual void Dispose(bool disposing) {
            this.Unhook();
        }

        #endregion


        private static class NativeMethods {

            internal const Int32 WH_KEYBOARD_LL = 13;
            internal const Int32 HC_ACTION = 0;


            [StructLayout(LayoutKind.Sequential)]
            internal struct KBDLLHOOKSTRUCT {
                public Int32 vkCode;
                public Int32 scanCode;
                public Int32 flags;
                public Int32 time;
                public UIntPtr dwExtraInfo;
            }


            [DebuggerDisplay("handle")]
            internal class WindowsHookSafeHandle : SafeHandleZeroOrMinusOneIsInvalid {
                public WindowsHookSafeHandle()
                    : base(true) {
                }

                protected override bool ReleaseHandle() {
                    return UnhookWindowsHookEx(this.handle);
                }
            }


            internal delegate IntPtr LowLevelKeyboardProc(Int32 nCode, IntPtr wParam, IntPtr lParam);


            [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
            internal static extern WindowsHookSafeHandle SetWindowsHookEx(Int32 idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, Int32 dwThreadId);

            [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
            [return: MarshalAsAttribute(UnmanagedType.Bool)]
            private static extern Boolean UnhookWindowsHookEx(IntPtr idHook);

            [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
            internal static extern IntPtr CallNextHookEx(WindowsHookSafeHandle hhk, int nCode, IntPtr wParam, IntPtr lParam);
        }

    }


    /// <summary>
    /// Low-level keyboard event arguments.
    /// </summary>
    public class LowLevelKeyboardHookCallbackEventArgs : EventArgs {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="virtualKeyCode">Virtual key code.</param>
        /// <param name="scanCode">Hardware scan code</param>
        /// <param name="flags">Key flags.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "flags", Justification = "Flags is used by Win32 on which this is based on.")]
        public LowLevelKeyboardHookCallbackEventArgs(int virtualKeyCode, int scanCode, int flags) {
            this.VirtualKeyCode = virtualKeyCode;
            this.ScanCode = scanCode;
            this.IsExtended = (flags & 0x01) != 0;
            this.IsInjectedFromLowerIntegrityLevel = (flags & 0x02) != 0;
            this.IsInjected = (flags & 0x10) != 0;
            this.IsAltPressed = (flags & 0x20) != 0;
            this.IsPressed = (flags & 0x80) == 0;
        }


        /// <summary>
        /// Gets a virtual key code.
        /// </summary>
        public int VirtualKeyCode { get; }

        /// <summary>
        /// Gets a hardware scan code for the key.
        /// </summary>
        public int ScanCode { get; }

        /// <summary>
        /// Specifies whether the key is an extended key, such as a function key or a key on the numeric keypad.
        /// </summary>
        public bool IsExtended { get; }

        /// <summary>
        /// Specifies whether the event was injected from a process running at lower integrity level.
        /// </summary>
        public bool IsInjectedFromLowerIntegrityLevel { get; }

        /// <summary>
        /// Specifies whether the event was injected.
        /// </summary>
        public bool IsInjected { get; }

        /// <summary>
        /// Specifies whether ALT key was pressed.
        /// </summary>
        public bool IsAltPressed { get; }

        /// <summary>
        /// Specifies whether key was pressed.
        /// </summary>
        public bool IsPressed { get; }
    }
}
