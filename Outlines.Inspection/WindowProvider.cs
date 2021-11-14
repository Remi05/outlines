using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using Outlines.Core;

namespace Outlines.Inspection
{
    public class WindowProvider : IWindowProvider
    {
        private IntPtr WindowToIgnore { get; set; }

        public WindowProvider(IntPtr windowToIgnore)
        {
            WindowToIgnore = windowToIgnore;
        }

        private List<IntPtr> GetWindows()
        {
            List<IntPtr> windows = new List<IntPtr>();
            NativeWindowService.EnumWindows(new NativeWindowService.EnumWindowsCallback((hwnd, _) =>
            {
                windows.Add(hwnd);
                return true;
            }), 0);
            return windows;
        }

        public IntPtr TryGetWindowFromPoint(Point point)
        {
            var windows = GetWindows();
            //var candidateWindows = windows.Where((hwnd) => ShouldConsiderWindow(hwnd));
            var candidateWindows = windows.Where((hwnd) => ShouldWindowCapturePoint(hwnd, point));
            //var candidateWindows = GetTopLevelWindowsAtPoint(point);
            var windowTitles = candidateWindows.Select(window => GetWindowString(window)); //.Where(windowTitle => !string.IsNullOrWhiteSpace(windowTitle));
            Logger.Log(LoggingLevel.Debug, $"Candidate Windows: {string.Join(", ", windowTitles)}");
            //return GetSmallestWindow(candidateWindows);
            return GetTopLevelWindowFromPoint(point);
        }

        private IntPtr GetSmallestWindow(IEnumerable<IntPtr> windows)
        {
            int minArea = int.MaxValue;
            IntPtr smallestWindow = windows.First();
            foreach (IntPtr window in windows)
            {
                var windowRect = GetWindowRect(window);
                int windowArea = windowRect.Width * windowRect.Height;
                if (windowArea < minArea)
                {
                    minArea = windowArea;
                    smallestWindow = window;
                }
            }
            return smallestWindow;
        }

        private IntPtr GetTopmostWindowFromPoint(Point point)
        {
            IntPtr curWindow = NativeWindowService.GetWindow(WindowToIgnore, NativeWindowService.WindowRelationship.GW_HWNDFIRST);
            while (curWindow != IntPtr.Zero)
            {
                if (ShouldWindowCapturePoint(curWindow, point))
                {
                    return curWindow;
                }
                curWindow = NativeWindowService.GetWindow(curWindow, NativeWindowService.WindowRelationship.GW_HWNDNEXT);
            }
            return IntPtr.Zero;
        }

        private IntPtr GetTopLevelWindowFromPoint(Point point)
        {
            IntPtr desktopWindow = NativeWindowService.GetDesktopWindow();
            IntPtr curWindow = NativeWindowService.GetWindow(desktopWindow, NativeWindowService.WindowRelationship.GW_CHILD);
            while (curWindow != IntPtr.Zero)
            {
                if (ShouldWindowCapturePoint(curWindow, point))
                {
                    return curWindow; 
                }
                curWindow = NativeWindowService.GetWindow(curWindow, NativeWindowService.WindowRelationship.GW_HWNDNEXT);
            }
            return IntPtr.Zero;
        }

        private List<IntPtr> GetTopLevelWindowsAtPoint(Point point)
        {
            List<IntPtr> windows = new List<IntPtr>();
            IntPtr desktopWindow = NativeWindowService.GetDesktopWindow();
            IntPtr curWindow = NativeWindowService.GetWindow(desktopWindow, NativeWindowService.WindowRelationship.GW_CHILD);
            while (curWindow != IntPtr.Zero)
            {
                if (ShouldWindowCapturePoint(curWindow, point))
                {
                    windows.Add(curWindow);
                }
                curWindow = NativeWindowService.GetWindow(curWindow, NativeWindowService.WindowRelationship.GW_HWNDNEXT);
            }
            return windows;
        }

        private string GetWindowString(IntPtr hwnd)
        {
            string title = GetWindowsTitle(hwnd);
            string className = GetWindowsClassName(hwnd);
            return $"[Title: {title}, ClassName: {className}]";
        }

        private string GetWindowsClassName(IntPtr hwnd)
        {
            int windowClassNameLength = 100;
            StringBuilder windowClassNameBuffer = new StringBuilder(windowClassNameLength);
            NativeWindowService.GetClassName(hwnd, windowClassNameBuffer, windowClassNameLength + 1);
            return windowClassNameBuffer.ToString();
        }

        private string GetWindowsTitle(IntPtr hwnd)
        {
            int windowTitleLength = NativeWindowService.GetWindowTextLength(hwnd);
            StringBuilder windowTitleBuffer = new StringBuilder(windowTitleLength);
            NativeWindowService.GetWindowText(hwnd, windowTitleBuffer, windowTitleLength + 1);
            return windowTitleBuffer.ToString();
        }

        private bool ShouldWindowCapturePoint(IntPtr hwnd, Point point)
        {
            return ShouldConsiderWindow(hwnd)
                && GetWindowRect(hwnd).Contains(point)
                && IsHitTestCaptured(hwnd, point);
        }

        private bool ShouldConsiderWindow(IntPtr hwnd)
        {
            return hwnd != WindowToIgnore
                && NativeWindowService.IsWindowEnabled(hwnd)
                && NativeWindowService.IsWindowVisible(hwnd)
                && !NativeWindowService.IsIconic(hwnd)
                && !IsWindowCloaked(hwnd)
                && !IsWindowTransparent(hwnd)
                && !IsWindowShowStateMinimized(hwnd);
        }

        private bool IsWindowCloaked(IntPtr hwnd)
        {
            uint isCloakedValue;
            NativeWindowService.DwmGetWindowAttribute(hwnd, NativeWindowService.DwmWindowAttributes.DWMWA_CLOAKED, out isCloakedValue, sizeof(uint));
            return isCloakedValue != 0;
        }

        private bool IsWindowTransparent(IntPtr hwnd)
        {
            int extendedWindowStyle = NativeWindowService.GetWindowLong(hwnd, NativeWindowService.WindowInfoIndices.GWL_EXSTYLE);
            return (extendedWindowStyle & (int)NativeWindowService.ExtendedWindowStyles.WS_EX_TRANSPARENT) != 0;
        }

        private bool IsWindowShowStateMinimized(IntPtr hwnd)
        {
            var windowPlacement = new NativeWindowService.WindowPlacement() { length = (uint)Marshal.SizeOf(typeof(NativeWindowService.WindowPlacement)) };
            NativeWindowService.GetWindowPlacement(hwnd, ref windowPlacement);
            return windowPlacement.showCmd == NativeWindowService.ShowWindowCommands.SW_SHOWMINIMIZED;
        }

        private bool IsHitTestCaptured(IntPtr hwnd, Point point)
        {
            int lParam = point.X | (point.Y << 16);
            int hitTestResult = NativeWindowService.SendMessage(hwnd, (uint)NativeWindowService.WindowMessages.WM_NCHITTEST, 0, lParam);
            return (hitTestResult != (int)NativeWindowService.HitTestResults.HTNOWHERE) 
                && (hitTestResult != (int)NativeWindowService.HitTestResults.HTTRANSPARENT)
                && (hitTestResult != (int)NativeWindowService.HitTestResults.HTERROR);
        }

        private Rectangle GetWindowRect(IntPtr hwnd)
        {
            NativeWindowService.RECT windowRect;
            NativeWindowService.GetWindowRect(hwnd, out windowRect);
            return new Rectangle(windowRect.left, windowRect.top, windowRect.right - windowRect.left, windowRect.bottom - windowRect.top);
        }
    }
}
