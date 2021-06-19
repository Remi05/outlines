using System.Collections.Generic;
using System.Drawing;

namespace Outlines.ImageProcessing
{
    public class LineDetectionService
    {
        private static readonly Color DefaultLineColor = Color.White;
        private const int DefaultMinLength = 2;
        private const int InvalidPos = -1;
        private Color LineColor { get; set; } = DefaultLineColor;
        private int MinLength { get; set; }

        public LineDetectionService(int minLength = DefaultMinLength)
        {
            MinLength = minLength;
        }

        public LineDetectionService(Color lineColor, int minLength = DefaultMinLength)
        {
            LineColor = lineColor;
            MinLength = minLength;
        }

        public List<Line> DetectLinesInImage(Bitmap image)
        {
            var lines = GetVerticalLines(image);
            lines.AddRange(GetHorizontalLines(image));
            return lines;
        }

        private List<Line> GetVerticalLines(Bitmap image)
        {
            var lines = new List<Line>();
            for (int x = 0; x < image.Width; ++x)
            {
                int start = InvalidPos;
                int end = InvalidPos;
                for (int y = 0; y < image.Height; ++y)
                {
                    if (image.GetPixel(x, y) == LineColor)
                    {
                        if (start == InvalidPos)
                        {
                            start = y;
                        }
                        else
                        {
                            end = y;
                        }
                    }
                    else if (start != InvalidPos)
                    {
                        if (end != InvalidPos && (end - start) >= MinLength)
                        {
                            lines.Add(new Line(new Point(x, start), new Point(x, end)));
                        }
                        start = InvalidPos;
                        end = InvalidPos;
                    }
                }
            }

            return lines;
        }

        private List<Line> GetHorizontalLines(Bitmap image)
        {
            var lines = new List<Line>();
            for (int y = 0; y < image.Height; ++y)
            {
                int start = InvalidPos;
                int end = InvalidPos;
                for (int x = 0; x < image.Width; ++x)
                {
                    if (image.GetPixel(x, y) == LineColor)
                    {
                        if (start == InvalidPos)
                        {
                            start = x;
                        }
                        else
                        {
                            end = x;
                        }
                    }
                    else if (start != InvalidPos)
                    {
                        if (end != InvalidPos && (end - start) >= MinLength)
                        {
                            lines.Add(new Line(new Point(start, y), new Point(end, y)));
                        }
                        start = InvalidPos;
                        end = InvalidPos;
                    }
                }
            }
            return lines;
        }
    }
}
