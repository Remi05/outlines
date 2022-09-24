using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Outlines.Inspection
{
    public class GlobalInputListener : IGlobalInputListener
    {
        private delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SetWindowsHookEx", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        private static extern int UnhookWindowsHookEx(IntPtr hhook);

        [DllImport("user32.dll")]
        private static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        // Based on https://docs.microsoft.com/en-us/previous-versions/dd162805(v=vs.85)
        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        // From https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexa
        private const int WH_KEYBOARD_LL = 13;

        private const int WM_KEYDOWN = 0x0100; // https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-keydown
        private const int WM_KEYUP = 0x0101; // https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-keyup

        private HookProc KeyboardHookProc { get; set; }
        private IntPtr KeyboardHookPtr { get; set; }

        public event KeyDownEventHandler KeyDown;
        public event KeyUpEventHandler KeyUp;

        public void RegisterToInputEvents()
        {
            KeyboardHookProc = new HookProc(KeyboardProc);
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                KeyboardHookPtr = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public void UnregisterFromInputEvents()
        {
            if (KeyboardHookPtr != IntPtr.Zero)
            {
                UnhookWindowsHookEx(KeyboardHookPtr);
                KeyboardHookPtr = IntPtr.Zero;
                KeyboardHookProc = null;
            }
        }

        private int KeyboardProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
            {
                return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            }

            int message = wParam.ToInt32();
            int vkCode = Marshal.ReadInt32(lParam); ;

            switch (message)
            {
                case WM_KEYDOWN:
                    KeyDown?.Invoke(vkCode);
                    break;
                case WM_KEYUP:
                    KeyUp?.Invoke(vkCode);
                    break;
            }

            return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        public Point GetCursorPosition()
        {
            return GetCursorPos(out POINT cursorPos) ? new Point(cursorPos.x, cursorPos.y) : Point.Empty;
        }
    }
}
