using System.Drawing;

namespace Outlines.Inspection
{
    public interface IInputMaskingService
    {
        bool IsInInputMask(Point screenPoint);
    }
}
