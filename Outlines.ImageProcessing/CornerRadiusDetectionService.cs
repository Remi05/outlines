using System;

namespace Outlines.ImageProcessing
{
    public class CornerRadiusMismatch : Exception
    {
        public override string Message => "The computed corner radius for one of the corners did not match vertically and horizontally.";
    }

    public class CornerRadiusDetectionService
    {
        public CornerRadius DetectCornerRadius(DetectedRectangle detectedRectangle)
        {
            int topLeft = detectedRectangle.Top.Start.X - detectedRectangle.Rect.Left;
            int topRight = detectedRectangle.Rect.Right - detectedRectangle.Top.End.X;
            int bottomRight = detectedRectangle.Rect.Right - detectedRectangle.Bottom.End.X;
            int bottomLeft = detectedRectangle.Bottom.Start.X - detectedRectangle.Rect.Left;

            if ((detectedRectangle.Left.Start.Y - detectedRectangle.Rect.Top) != topLeft
             || (detectedRectangle.Right.Start.Y - detectedRectangle.Rect.Top) != topRight
             || (detectedRectangle.Rect.Bottom - detectedRectangle.Right.End.Y) != bottomRight
             || (detectedRectangle.Rect.Bottom - detectedRectangle.Left.End.Y) != bottomLeft)
            {
                throw new CornerRadiusMismatch();
            }

            return new CornerRadius(topLeft, topRight, bottomRight, bottomLeft);
        }
    }
}
