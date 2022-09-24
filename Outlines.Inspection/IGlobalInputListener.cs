using System.Drawing;

namespace Outlines.Inspection
{
    public delegate void KeyDownEventHandler(int vkCode);
    public delegate void KeyUpEventHandler(int vkCode);

    public interface IGlobalInputListener
    {
        event KeyDownEventHandler KeyDown;
        event KeyUpEventHandler KeyUp;

        void RegisterToInputEvents();
        void UnregisterFromInputEvents();

        Point GetCursorPosition();
    }
}