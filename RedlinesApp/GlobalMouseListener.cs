using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace RedlinesApp
{
    public delegate void MouseMovedEventHandler();
    public delegate void MouseDownEventHandler();

    public class GlobalMouseListener
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

        private const int MK_LBUTTON = 0x0001;
        private const int MK_CONTROL = 0x0008;

        private HookProc MouseHookProc { get; set; }
        private IntPtr MouseHookPtr { get; set; }

        public MouseMovedEventHandler MouseMoved;
        public MouseDownEventHandler MouseDown;

        public void RegisterToMouseEvents()
        {
            MouseHookProc = new HookProc(MouseProc);

            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                MouseHookPtr = SetWindowsHookEx(WH_MOUSE_LL, MouseHookProc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public void UnregisterFromMouseEvents()
        {
            if (MouseHookPtr != IntPtr.Zero)
            {
                UnhookWindowsHookEx(MouseHookPtr);
                MouseHookPtr = IntPtr.Zero;
                MouseHookProc = null;
            }
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
