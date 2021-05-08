using System.Windows;

namespace Outlines
{
    public delegate void MouseMovedEventHandler(Point cursorPos);
    public delegate void MouseDownEventHandler(Point cursorPos);
    public delegate void KeyDownEventHandler(int vkCode);
    public delegate void KeyUpEventHandler(int vkCode);

    public interface IGlobalInputListener
    {
        event KeyDownEventHandler KeyDown;
        event KeyUpEventHandler KeyUp;
        event MouseDownEventHandler MouseDown;
        event MouseMovedEventHandler MouseMoved;

        void RegisterToInputEvents();
        void UnregisterFromInputEvents();
    }
}