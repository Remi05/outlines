using System.Drawing;
using Outlines.Core;

namespace Outlines.Inspection
{
    public interface IScreenshotService
    {
        Image TakeScreenshot(ElementProperties elementProperties);
        Image TakeScreenshot(Rectangle bounds);
    }
}