using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OutlinesApp
{
    public delegate void MouseMovedEventHandler();
    public delegate void MouseDownEventHandler();
    public delegate void KeyDownEventHandler(Keys key);
    public delegate void KeyUpEventHandler(Keys key);

    public class GlobalInputListener
    {
        private delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SetWindowsHookEx", SetLastError = true)]
        static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")] 
        public static extern int UnhookWindowsHookEx(IntPtr hhook);

        [DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr GetModuleHandle(string lpModuleName);

        // From https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexa
        private const int WH_CALLWNDPROC = 4;
        private const int WH_KEYBOARD = 2;
        private const int WH_KEYBOARD_LL = 13;
        private const int WH_MOUSE = 7;
        private const int WH_MOUSE_LL = 14;

        private const int WM_MOUSEMOVE = 0x0200; // https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-mousemove
        private const int WM_LBUTTONDOWN = 0x0201; //https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-lbuttondown
        private const int WM_KEYDOWN = 0x0100; // https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-keydown
        private const int WM_KEYUP = 0x0101; // https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-keyup

        private const int MK_LBUTTON = 0x0001;
        private const int MK_CONTROL = 0x0008;

        private HookProc KeyboardHookProc { get; set; }
        private HookProc MouseHookProc { get; set; }
        private IntPtr KeyboardHookPtr { get; set; }
        private IntPtr MouseHookPtr { get; set; }

        public KeyDownEventHandler KeyDown;
        public KeyUpEventHandler KeyUp;
        public MouseDownEventHandler MouseDown;
        public MouseMovedEventHandler MouseMoved;

        public void RegisterToInputEvents()
        {
            KeyboardHookProc = new HookProc(KeyboardProc);
            MouseHookProc = new HookProc(MouseProc);

            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                KeyboardHookPtr = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProc, GetModuleHandle(curModule.ModuleName), 0);
                MouseHookPtr = SetWindowsHookEx(WH_MOUSE_LL, MouseHookProc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public void UnregisterFromInputEvents()
        {
            if (MouseHookPtr != IntPtr.Zero)
            {
                UnhookWindowsHookEx(KeyboardHookPtr);
                UnhookWindowsHookEx(MouseHookPtr); 
                KeyboardHookPtr = IntPtr.Zero;
                MouseHookPtr = IntPtr.Zero;
                KeyboardHookProc = null;
                MouseHookProc = null;
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
            Keys key = (Keys)vkCode;

            switch (message)
            {
                case WM_KEYDOWN:
                    KeyDown?.Invoke(key);
                    break;
                case WM_KEYUP:
                    KeyUp?.Invoke(key);
                    break;
            }

            return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        private int MouseProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
            {
                return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            }

            int message = wParam.ToInt32();

            switch (message)
            {
                case WM_MOUSEMOVE:
                    MouseMoved?.Invoke();
                    break;
                case WM_LBUTTONDOWN:
                    MouseDown?.Invoke();
                    break;
            }

            return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }
    }
}
