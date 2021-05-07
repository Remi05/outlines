namespace Outlines
{
    public delegate void MouseMovedEventHandler(int cursorX, int cursorY);
    public delegate void MouseDownEventHandler(int cursorX, int cursorY);
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