using System.Collections.Generic;
using System.Windows;

namespace Redlines
{
    public class DistanceOutlinesProvider
    {
        public List<DistanceOutline> GetDistanceOutlines(ElementProperties selectedElement, ElementProperties targetElement)
        {
            List<DistanceOutline> distanceOutlines = new List<DistanceOutline>();

            if (selectedElement == null || targetElement == null)
            {
                return distanceOutlines;
            }

            bool isSelectedContained = targetElement.BoundingRect.Contains(selectedElement.BoundingRect);
            bool isTargetContained = selectedElement.BoundingRect.Contains(targetElement.BoundingRect);

            if (isSelectedContained || isTargetContained)
            {
                Rect containingRect = isSelectedContained ? targetElement.BoundingRect : selectedElement.BoundingRect;
                Rect containedRect = isSelectedContained ? selectedElement.BoundingRect : targetElement.BoundingRect;
                distanceOutlines.AddRange(GetContainedDistanceOutlines(containingRect, containedRect));
            }
            else
            {

                Vector selectedElementDiag = Point.Subtract(selectedElement.BoundingRect.BottomRight, selectedElement.BoundingRect.TopLeft);
                Point selectedElementCenter = Point.Add(selectedElement.BoundingRect.TopLeft, selectedElementDiag / 2);

                distanceOutlines.AddRange(GetVerticalOffsetDistanceOutlines(selectedElement, targetElement, selectedElementCenter));
                distanceOutlines.AddRange(GetHorizontalOffsetDistanceOutlines(selectedElement, targetElement, selectedElementCenter));
            }

            return distanceOutlines;
        }

        private List<DistanceOutline> GetContainedDistanceOutlines(Rect containingRect, Rect containedRect)
        {
            Vector containedDiag = Point.Subtract(containedRect.BottomRight, containedRect.TopLeft);
            Point containedCenter = Point.Add(containedRect.TopLeft, containedDiag / 2);
            Point containedTopCenter = new Point(containedCenter.X, containedRect.Top);
            Point containedBottomCenter = new Point(containedCenter.X, containedRect.Bottom);
            Point containedLeftCenter = new Point(containedRect.Left, containedCenter.Y);
            Point containedRightCenter = new Point(containedRect.Right, containedCenter.Y);

            var topToTopOutline = new DistanceOutline(containedTopCenter, new Point(containedTopCenter.X, containingRect.Top));
            var bottomToBottomOutline = new DistanceOutline(containedBottomCenter, new Point(containedBottomCenter.X, containingRect.Bottom));
            var leftToLeftOutline = new DistanceOutline(containedLeftCenter, new Point(containingRect.Left, containedLeftCenter.Y));
            var rightToRightOutline = new DistanceOutline(containedRightCenter, new Point(containingRect.Right, containedRightCenter.Y));

            return new List<DistanceOutline>() 
            { 
                topToTopOutline,
                bottomToBottomOutline,
                leftToLeftOutline,
                rightToRightOutline
            };
        }

        private List<DistanceOutline> GetVerticalOffsetDistanceOutlines(ElementProperties selectedElement, ElementProperties targetElement, Point selectedElementCenter)
        {
            List<DistanceOutline> distanceOutlines = new List<DistanceOutline>();

            if (selectedElement.BoundingRect.Top > targetElement.BoundingRect.Bottom)
            {
                // Top to Bottom
                var selectedTopCenter = new Point(selectedElementCenter.X, selectedElement.BoundingRect.Top);
                var lineEnd = new Point(selectedElementCenter.X, targetElement.BoundingRect.Bottom);
                var topToBottomOutline = new DistanceOutline(selectedTopCenter, lineEnd);
                distanceOutlines.Add(topToBottomOutline);

                distanceOutlines.AddRange(GetVerticalAligmentLines(targetElement.BoundingRect, selectedElement.BoundingRect));

                if (lineEnd.X < targetElement.BoundingRect.Left)
                {
                    distanceOutlines.Add(new DistanceOutline(lineEnd, targetElement.BoundingRect.BottomLeft, true));
                }
                else if (lineEnd.X > targetElement.BoundingRect.Right)
                {
                    distanceOutlines.Add(new DistanceOutline(lineEnd, targetElement.BoundingRect.BottomRight, true));
                }
            }
            else if (selectedElement.BoundingRect.Bottom < targetElement.BoundingRect.Top)
            {
                // Bottom to Top
                var selectedBottomCenter = new Point(selectedElementCenter.X, selectedElement.BoundingRect.Bottom);
                var lineEnd = new Point(selectedElementCenter.X, targetElement.BoundingRect.Top);
                var bottomToTopOutline = new DistanceOutline(selectedBottomCenter, lineEnd);
                distanceOutlines.Add(bottomToTopOutline);

                distanceOutlines.AddRange(GetVerticalAligmentLines(selectedElement.BoundingRect, targetElement.BoundingRect));

                if (lineEnd.X < targetElement.BoundingRect.Left)
                {
                    distanceOutlines.Add(new DistanceOutline(lineEnd, targetElement.BoundingRect.TopLeft, true));
                }
                else if (lineEnd.X > targetElement.BoundingRect.Right)
                {
                    distanceOutlines.Add(new DistanceOutline(lineEnd, targetElement.BoundingRect.TopRight, true));
                }
            }

            return distanceOutlines;
        }

        private List<DistanceOutline> GetHorizontalOffsetDistanceOutlines(ElementProperties selectedElement, ElementProperties targetElement, Point selectedElementCenter)
        {
            List<DistanceOutline> distanceOutlines = new List<DistanceOutline>();

            if (selectedElement.BoundingRect.Left > targetElement.BoundingRect.Right)
            {
                // Left to Right
                var selectedLeftCenter = new Point(selectedElement.BoundingRect.Left, selectedElementCenter.Y);
                var lineEnd = new Point(targetElement.BoundingRect.Right, selectedElementCenter.Y);
                var leftToRightOutline = new DistanceOutline(selectedLeftCenter, lineEnd);
                distanceOutlines.Add(leftToRightOutline);

                distanceOutlines.AddRange(GetHorizontalAligmentLines(targetElement.BoundingRect, selectedElement.BoundingRect));

                if (lineEnd.Y < targetElement.BoundingRect.Top)
                {
                    distanceOutlines.Add(new DistanceOutline(lineEnd, targetElement.BoundingRect.TopRight, true));
                }
                else if (lineEnd.Y > targetElement.BoundingRect.Bottom)
                {
                    distanceOutlines.Add(new DistanceOutline(lineEnd, targetElement.BoundingRect.BottomRight, true));
                }
            }
            else if (selectedElement.BoundingRect.Right < targetElement.BoundingRect.Left)
            {
                // Right to Left
                var selectedRightCenter = new Point(selectedElement.BoundingRect.Right, selectedElementCenter.Y);
                var lineEnd = new Point(targetElement.BoundingRect.Left, selectedElementCenter.Y);
                var rightToLeftOutline = new DistanceOutline(selectedRightCenter, new Point(targetElement.BoundingRect.Left, selectedElementCenter.Y));
                distanceOutlines.Add(rightToLeftOutline);

                distanceOutlines.AddRange(GetHorizontalAligmentLines(selectedElement.BoundingRect, targetElement.BoundingRect));

                if (lineEnd.Y < targetElement.BoundingRect.Top)
                {
                    distanceOutlines.Add(new DistanceOutline(lineEnd, targetElement.BoundingRect.TopLeft, true));
                }
                else if (lineEnd.Y > targetElement.BoundingRect.Bottom)
                {
                    distanceOutlines.Add(new DistanceOutline(lineEnd, targetElement.BoundingRect.BottomLeft, true));
                }
            }

            return distanceOutlines;
        }

        private List<DistanceOutline> GetHorizontalAligmentLines(Rect leftRect, Rect rightRect)
        {
            List<DistanceOutline> distanceOutlines = new List<DistanceOutline>();

            if (leftRect.Top == rightRect.Top)
            {
                distanceOutlines.Add(new DistanceOutline(leftRect.TopRight, rightRect.TopLeft, true));
            }

            if (leftRect.Bottom == rightRect.Bottom)
            {
                distanceOutlines.Add(new DistanceOutline(leftRect.BottomRight, rightRect.BottomLeft, true));
            }

            return distanceOutlines;
        }

        private List<DistanceOutline> GetVerticalAligmentLines(Rect topRect, Rect bottomRect)
        {
            List<DistanceOutline> distanceOutlines = new List<DistanceOutline>();

            if (topRect.Left == bottomRect.Left)
            {
                distanceOutlines.Add(new DistanceOutline(topRect.BottomLeft, bottomRect.TopLeft, true));
            }

            if (topRect.Right == bottomRect.Right)
            {
                distanceOutlines.Add(new DistanceOutline(topRect.BottomRight, bottomRect.TopRight, true));
            }

            return distanceOutlines;
        }
    }
}
