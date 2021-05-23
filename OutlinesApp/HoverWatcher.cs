using System.Timers;
using System.Windows;

namespace OutlinesApp
{
    public delegate void MouseHoveredDelegate(Point cursorPos);

    public class HoverWatcher
    {
        private Point LastCursorPos { get; set; }

        private int HoverDelayInMs { get; set; }

        private Timer HoverTimer { get; set; } = new Timer();

        public event MouseHoveredDelegate MouseHovered;

        public HoverWatcher(int hoverDelayInMs)
        {
            HoverDelayInMs = hoverDelayInMs;
            HoverTimer.Interval = HoverDelayInMs;
            HoverTimer.AutoReset = false;
            HoverTimer.Elapsed += (_, __) => OnTimerElapsed();
        }

        private void OnTimerElapsed()
        {
            MouseHovered(LastCursorPos);
        }

        public void OnMouseMoved(Point cursorPos)
        {
            HoverTimer.Stop();
            LastCursorPos = cursorPos;
            HoverTimer.Start();
        }
    }
}
