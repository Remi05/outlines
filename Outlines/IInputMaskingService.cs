using System.Windows;

namespace Outlines
{
    public interface IInputMaskingService
    {
        bool IsInInputMask(Point screenPoint);
    }
}
