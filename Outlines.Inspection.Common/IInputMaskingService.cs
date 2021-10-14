using System.Drawing;

namespace Outlines.Inspection.Common
{
    public interface IInputMaskingService
    {
        bool IsInInputMask(Point screenPoint);
    }
}
