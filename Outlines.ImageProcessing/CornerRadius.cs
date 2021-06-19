
namespace Outlines.ImageProcessing
{
    public class CornerRadius
    {
        public int TopLeft { get; private set; }
        public int TopRight { get; private set; }
        public int BottomRight { get; private set; }
        public int BottomLeft { get; private set; }

        public CornerRadius(int topLeft, int topRight, int bottomRight, int bottomLeft)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomRight = bottomRight;
            BottomLeft = bottomLeft;
        }
    }
}
