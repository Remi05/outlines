using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Outlines.Inspection
{
    public static class NativeWindowService
    {
        // Based on https://docs.microsoft.com/en-us/previous-versions/dd162805(v=vs.85)
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/windef/ns-windef-rect
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-windowplacement
        [StructLayout(LayoutKind.Sequential)]
        public struct WindowPlacement
        {
            public uint length;
            public uint flags;
            public ShowWindowCommands showCmd;
            public POINT ptMinPosition;
            public POINT ptMaxPosition;
            public RECT rcNormalPosition;
            public RECT rcDevice;
        }

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-childwindowfrompointex
        public enum ChildWindowFromPointFlags : uint
        {
            CWP_ALL = 0x0000,
            CWP_SKIPINVISIBLE = 0x0001,
            CWP_SKIPDISABLED = 0x0002,
            CWP_SKIPTRANSPARENT = 0x0004
        }

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsystemmetrics
        public enum SystemMetricsIndices
        {
            SM_XVIRTUALSCREEN = 76,
            SM_YVIRTUALSCREEN = 77,
            SM_CXVIRTUALSCREEN = 78,
            SM_CYVIRTUALSCREEN = 79,
        }

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowlonga
        public enum WindowInfoIndices
        {
            GWL_EXSTYLE = -20,
            GWL_HINSTANCE = -6,
            GWL_HWNDPARENT = -8,
            GWL_ID = -12,
            GWL_STYLE = -16,
            GWL_USERDATA = -21,
            GWL_WNDPROC = -4,
        }

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindow
        public enum WindowRelationship
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6,
        }

        // Based on https://docs.microsoft.com/en-us/windows/win32/winmsg/window-styles
        public enum WindowStyles : uint
        {
            WS_BORDER = 0x00800000,
            WS_CAPTION = 0x00C00000,
            WS_CHILD = 0x40000000,
            WS_CHILDWINDOW = 0x40000000,
            WS_CLIPCHILDREN = 0x02000000,
            WS_CLIPSIBLINGS = 0x04000000,
            WS_DISABLED = 0x08000000,
            WS_DLGFRAME = 0x00400000,
            WS_GROUP = 0x00020000,
            WS_HSCROLL = 0x00100000,
            WS_ICONIC = 0x20000000,
            WS_MAXIMIZE = 0x01000000,
            WS_MAXIMIZEBOX = 0x00010000,
            WS_MINIMIZE = 0x20000000,
            WS_MINIMIZEBOX = 0x00020000,
            WS_OVERLAPPED = 0x00000000,
            WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
            WS_POPUP = 0x80000000,
            WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,
            WS_SIZEBOX = 0x00040000,
            WS_SYSMENU = 0x00080000,
            WS_TABSTOP = 0x00010000,
            WS_THICKFRAME = 0x00040000,
            WS_TILED = 0x00000000,
            WS_TILEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
            WS_VISIBLE = 0x10000000,
            WS_VSCROLL = 0x00200000,
        }

        // Based on https://docs.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles
        public enum ExtendedWindowStyles : uint
        {
            WS_EX_ACCEPTFILES = 0x00000010,
            WS_EX_APPWINDOW = 0x00040000,
            WS_EX_CLIENTEDGE = 0x00000200,
            WS_EX_COMPOSITED = 0x02000000,
            WS_EX_CONTEXTHELP = 0x00000400,
            WS_EX_CONTROLPARENT = 0x00010000,
            WS_EX_DLGMODALFRAME = 0x00000001,
            WS_EX_LAYERED = 0x00080000,
            WS_EX_LAYOUTRTL = 0x00400000,
            WS_EX_LEFT = 0x00000000,
            WS_EX_LEFTSCROLLBAR = 0x00004000,
            WS_EX_LTRREADING = 0x00000000,
            WS_EX_MDICHILD = 0x00000040,
            WS_EX_NOACTIVATE = 0x08000000,
            WS_EX_NOINHERITLAYOUT = 0x00100000,
            WS_EX_NOPARENTNOTIFY = 0x00000004,
            WS_EX_NOREDIRECTIONBITMAP = 0x00200000,
            WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,
            WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,
            WS_EX_RIGHT = 0x00001000,
            WS_EX_RIGHTSCROLLBAR = 0x00000000,
            WS_EX_RTLREADING = 0x00002000,
            WS_EX_STATICEDGE = 0x00020000,
            WS_EX_TOOLWINDOW = 0x00000080,
            WS_EX_TOPMOST = 0x00000008,
            WS_EX_TRANSPARENT = 0x00000020,
            WS_EX_WINDOWEDGE = 0x00000100,
        }

        // Based on https://docs.microsoft.com/en-us/windows/win32/winmsg/about-messages-and-message-queues
        public enum WindowMessages : uint
        {
            WM_NCHITTEST = 0x0084,
        }

        // Based on https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-nchittest
        public enum HitTestResults : int
        {
            HTERROR = -2,
            HTTRANSPARENT = -1,
            HTNOWHERE = 0,
            HTCLIENT = 1,
            HTCAPTION = 2,
            HTSYSMENU = 3,
            HTGROWBOX = 4,
            HTSIZE = 4,
            HTMENU = 5,
            HTHSCROLL = 6,
            HTVSCROLL = 7,
            HTMINBUTTON = 8,
            HTREDUCE = 8,
            HTMAXBUTTON = 9,
            HTZOOM = 9,
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17,
            HTBORDER = 18,
            HTCLOSE = 20,
            HTHELP = 21,
        }

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/dwmapi/ne-dwmapi-dwmwindowattribute
        public enum DwmWindowAttributes : int
        {
            DWMWA_CLOAKED = 14,
            DWMWA_CAPTION_COLOR = 35,
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            DWMWA_MICA_EFFECT = 1029,
        }
        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
        public enum ShowWindowCommands : uint
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
        }

        public delegate bool EnumWindowsCallback(IntPtr hwnd, int lParam);

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumwindows
        [DllImport("user32.dll")]
        public static extern bool EnumWindows(EnumWindowsCallback lpEnumFunc, int lParam);

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-findwindowa
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string windowName);

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getdesktopwindow
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getforegroundwindow
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-gettopwindow
        [DllImport("user32.dll")]
        public static extern IntPtr GetTopWindow(IntPtr hwnd);

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindow
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hwnd, WindowRelationship windowRelationship);

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowrect
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT rect);

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowplacement
        [DllImport("user32.dll")]
        public static extern bool GetWindowPlacement(IntPtr hwnd, ref WindowPlacement windowPlacement);

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getclassname
        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr hwnd, StringBuilder windowClassNameBuffer, int bufferSize);

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtexta
        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hwnd, StringBuilder windowTextBuffer, int bufferSize);

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtextlengtha
        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hwnd);

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowlongw
        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, WindowInfoIndices infoIndex);

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowlongw
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowLong(IntPtr hwnd, WindowInfoIndices infoIndex, int infoValue);

        // Based on ...
        [DllImport("user32.dll")]
        public static extern bool SetProp(IntPtr hwnd, string propertyName, IntPtr propertyData);

        [DllImport("user32.dll")]
        public static extern IntPtr GetProp(IntPtr hwnd, string propertyName);

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-isiconic
        [DllImport("user32.dll")]
        public static extern bool IsIconic(IntPtr hwnd); 

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-iswindowenabled
        [DllImport("user32.dll")]
        public static extern bool IsWindowEnabled(IntPtr hwnd);

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-iswindowvisible
        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hwnd);

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-movewindow
        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hwnd, int x, int y, int width, int height, bool shouldRepaint);

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-childwindowfrompointex
        [DllImport("user32.dll")]
        public static extern IntPtr ChildWindowFromPointEx(IntPtr hwnd, POINT pt, ChildWindowFromPointFlags flags);

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsystemmetrics
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(SystemMetricsIndices systemMetricsIndex);

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendmessage
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, uint message, int wParam, int lParam);

        // Based on https://docs.microsoft.com/en-us/windows/win32/api/dwmapi/nf-dwmapi-dwmgetwindowattribute
        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, DwmWindowAttributes attribute, out uint attributeValue, int attributeValueSize);
        
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttributes attribute, uint attributeValue, int attributeValueSize);
    }
}
