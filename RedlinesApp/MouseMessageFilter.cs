using System.Windows.Forms;

namespace RedlinesApp
{
    public delegate void MouseMovedEventHandler();
    public delegate void MouseDownEventHandler();

    public class MouseMessageFilter : IMessageFilter
    {
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_MOUSEMOVE = 0x0200;

        public event MouseMovedEventHandler MouseMoved;
        public event MouseDownEventHandler MouseDown;

        public bool PreFilterMessage(ref Message message)
        {
            switch (message.Msg)
            {
                case WM_LBUTTONDOWN:
                    MouseDown?.Invoke();
                    break;
                case WM_MOUSEMOVE:
                    MouseMoved?.Invoke();
                    break;
            }
            return false;
        }
    }
}
