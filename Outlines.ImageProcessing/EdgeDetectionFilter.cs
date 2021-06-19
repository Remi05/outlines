using System.Drawing;

namespace Outlines.ImageProcessing
{
    public class EdgeDetectionFilter
    {
        public Bitmap Apply(Bitmap image)
        {
            var imageWithFilter = new Bitmap(image);
            return imageWithFilter;
        }
    }
}
