using System.Collections.Generic;
using System.Drawing;

namespace Outlines.Core
{
    public class DistanceOutlinesProvider : IDistanceOutlinesProvider
    {
        public List<DistanceOutline> GetDistanceOutlines(ElementProperties selectedElement, ElementProperties targetElement)
        {
            List<DistanceOutline> distanceOutlines = new List<DistanceOutline>();

            if (selectedElement == null || selectedElement.BoundingRect == Rectangle.Empty
             || targetElement == null || targetElement.BoundingRect == Rectangle.Empty)
            {
                return distanceOutlines;
            }

            bool isSelectedContained = targetElement.BoundingRect.Contains(selectedElement.BoundingRect);
            bool isTargetContained = selectedElement.BoundingRect.Contains(targetElement.BoundingRect);

            if (isSelectedContained || isTargetContained)
            {
                Rectangle containingRect = isSelectedContained ? targetElement.BoundingRect : selectedElement.BoundingRect;
                Rectangle containedRect = isSelectedContained ? selectedElement.BoundingRect : targetElement.BoundingRect;
                distanceOutlines.AddRange(GetContainedDistanceOutlines(containingRect, containedRect));
            }
            else
            {

                Point selectedElementDiag = selectedElement.BoundingRect.BottomRight().Subtract(selectedElement.BoundingRect.TopLeft());
                Point selectedElementCenter = selectedElement.BoundingRect.TopLeft().Add(selectedElementDiag.Divide(2));

                distanceOutlines.AddRange(GetVerticalOffsetDistanceOutlines(selectedElement, targetElement, selectedElementCenter));
                distanceOutlines.AddRange(GetHorizontalOffsetDistanceOutlines(selectedElement, targetElement, selectedElementCenter));
            }

            return distanceOutlines;
        }

        private List<DistanceOutline> GetContainedDistanceOutlines(Rectangle containingRect, Rectangle containedRect)
        {
            Point containedDiag = containedRect.BottomRight().Subtract(containedRect.TopLeft());
            Point containedCenter = containedRect.TopLeft().Add(containedDiag.Divide(2));
            Point containedTopCenter = new Point(containedCenter.X, containedRect.Top);
            Point containedBottomCenter = new Point(containedCenter.X, containedRect.Bottom);
            Point containedLeftCenter = new Point(containedRect.Left, containedCenter.Y);
            Point containedRightCenter = new Point(containedRect.Right, containedCenter.Y);

            Point containingDiag = containingRect.BottomRight().Subtract(containingRect.TopLeft());
            Point containingCenter = containingRect.TopLeft().Add(containingDiag.Divide(2));

            bool isCenterAlignedHorizontally = (containingCenter.X == containedCenter.X);
            bool isCenterAlignedVertically = (containingCenter.Y == containedCenter.Y);

            var topToTopOutline = new DistanceOutline(containedTopCenter, new Point(containedTopCenter.X, containingRect.Top), isDistanceLine: true, isAlignmentLine: isCenterAlignedHorizontally);
            var bottomToBottomOutline = new DistanceOutline(containedBottomCenter, new Point(containedBottomCenter.X, containingRect.Bottom), isDistanceLine: true, isAlignmentLine: isCenterAlignedHorizontally);
            var leftToLeftOutline = new DistanceOutline(containedLeftCenter, new Point(containingRect.Left, containedLeftCenter.Y), isDistanceLine: true, isAlignmentLine: isCenterAlignedVertically);
            var rightToRightOutline = new DistanceOutline(containedRightCenter, new Point(containingRect.Right, containedRightCenter.Y), isDistanceLine: true, isAlignmentLine: isCenterAlignedVertically);

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
            double targetElementCenterX = targetElement.BoundingRect.Left + (targetElement.BoundingRect.Width / 2.0);
            bool isCenterAlignedHorizontally = selectedElementCenter.X == (int)targetElementCenterX; // Consider center-alignemnt at the pixel level.

            if (selectedElement.BoundingRect.Top > targetElement.BoundingRect.Bottom)
            {
                // Top to Bottom
                var selectedTopCenter = new Point(selectedElementCenter.X, selectedElement.BoundingRect.Top);
                var lineEnd = new Point(selectedElementCenter.X, targetElement.BoundingRect.Bottom);
                var topToBottomOutline = new DistanceOutline(selectedTopCenter, lineEnd, isDistanceLine: true, isAlignmentLine: isCenterAlignedHorizontally);
                distanceOutlines.Add(topToBottomOutline);

                distanceOutlines.AddRange(GetVerticalAligmentLines(targetElement.BoundingRect, selectedElement.BoundingRect));

                if (lineEnd.X < targetElement.BoundingRect.Left)
                {
                    distanceOutlines.Add(new DistanceOutline(lineEnd, targetElement.BoundingRect.BottomLeft(), isDistanceLine: false, isAlignmentLine: false));
                }
                else if (lineEnd.X > targetElement.BoundingRect.Right)
                {
                    distanceOutlines.Add(new DistanceOutline(lineEnd, targetElement.BoundingRect.BottomRight(), isDistanceLine: false, isAlignmentLine: false));
                }
            }
            else if (selectedElement.BoundingRect.Bottom < targetElement.BoundingRect.Top)
            {
                // Bottom to Top
                var selectedBottomCenter = new Point(selectedElementCenter.X, selectedElement.BoundingRect.Bottom);
                var lineEnd = new Point(selectedElementCenter.X, targetElement.BoundingRect.Top);
                var bottomToTopOutline = new DistanceOutline(selectedBottomCenter, lineEnd, isDistanceLine: true, isAlignmentLine: isCenterAlignedHorizontally);
                distanceOutlines.Add(bottomToTopOutline);

                distanceOutlines.AddRange(GetVerticalAligmentLines(selectedElement.BoundingRect, targetElement.BoundingRect));

                if (lineEnd.X < targetElement.BoundingRect.Left)
                {
                    distanceOutlines.Add(new DistanceOutline(lineEnd, targetElement.BoundingRect.TopLeft(), isDistanceLine: false, isAlignmentLine: false));
                }
                else if (lineEnd.X > targetElement.BoundingRect.Right)
                {
                    distanceOutlines.Add(new DistanceOutline(lineEnd, targetElement.BoundingRect.TopRight(), isDistanceLine: false, isAlignmentLine: false));
                }
            }

            return distanceOutlines;
        }

        private List<DistanceOutline> GetHorizontalOffsetDistanceOutlines(ElementProperties selectedElement, ElementProperties targetElement, Point selectedElementCenter)
        {
            List<DistanceOutline> distanceOutlines = new List<DistanceOutline>();
            double targetElementCenterY = targetElement.BoundingRect.Top + (targetElement.BoundingRect.Height / 2.0);
            bool isCenterAlignedVertically = selectedElementCenter.Y == (int)targetElementCenterY; // Consider center-alignemnt at the pixel level.

            if (selectedElement.BoundingRect.Left > targetElement.BoundingRect.Right)
            {
                // Left to Right
                var selectedLeftCenter = new Point(selectedElement.BoundingRect.Left, selectedElementCenter.Y);
                var lineEnd = new Point(targetElement.BoundingRect.Right, selectedElementCenter.Y);
                var leftToRightOutline = new DistanceOutline(selectedLeftCenter, lineEnd, isDistanceLine: true, isAlignmentLine: isCenterAlignedVertically);
                distanceOutlines.Add(leftToRightOutline);

                distanceOutlines.AddRange(GetHorizontalAligmentLines(targetElement.BoundingRect, selectedElement.BoundingRect));

                if (lineEnd.Y < targetElement.BoundingRect.Top)
                {
                    distanceOutlines.Add(new DistanceOutline(lineEnd, targetElement.BoundingRect.TopRight(), isDistanceLine: false, isAlignmentLine: false));
                }
                else if (lineEnd.Y > targetElement.BoundingRect.Bottom)
                {
                    distanceOutlines.Add(new DistanceOutline(lineEnd, targetElement.BoundingRect.BottomRight(), isDistanceLine: false, isAlignmentLine: false));
                }
            }
            else if (selectedElement.BoundingRect.Right < targetElement.BoundingRect.Left)
            {
                // Right to Left
                var selectedRightCenter = new Point(selectedElement.BoundingRect.Right, selectedElementCenter.Y);
                var lineEnd = new Point(targetElement.BoundingRect.Left, selectedElementCenter.Y);
                var rightToLeftOutline = new DistanceOutline(selectedRightCenter, lineEnd, isDistanceLine: true, isAlignmentLine: isCenterAlignedVertically);
                distanceOutlines.Add(rightToLeftOutline);

                distanceOutlines.AddRange(GetHorizontalAligmentLines(selectedElement.BoundingRect, targetElement.BoundingRect));

                if (lineEnd.Y < targetElement.BoundingRect.Top)
                {
                    distanceOutlines.Add(new DistanceOutline(lineEnd, targetElement.BoundingRect.TopLeft(), isDistanceLine: false, isAlignmentLine: false));
                }
                else if (lineEnd.Y > targetElement.BoundingRect.Bottom)
                {
                    distanceOutlines.Add(new DistanceOutline(lineEnd, targetElement.BoundingRect.BottomLeft(), isDistanceLine: false, isAlignmentLine: false));
                }
            }

            return distanceOutlines;
        }

        private List<DistanceOutline> GetHorizontalAligmentLines(Rectangle leftRect, Rectangle rightRect)
        {
            List<DistanceOutline> distanceOutlines = new List<DistanceOutline>();

            if (leftRect.Top == rightRect.Top)
            {
                distanceOutlines.Add(new DistanceOutline(leftRect.TopRight(), rightRect.TopLeft(), isDistanceLine: false, isAlignmentLine: true));
            }

            if (leftRect.Bottom == rightRect.Bottom)
            {
                distanceOutlines.Add(new DistanceOutline(leftRect.BottomRight(), rightRect.BottomLeft(), isDistanceLine: false, isAlignmentLine: true));
            }

            return distanceOutlines;
        }

        private List<DistanceOutline> GetVerticalAligmentLines(Rectangle topRect, Rectangle bottomRect)
        {
            List<DistanceOutline> distanceOutlines = new List<DistanceOutline>();

            if (topRect.Left == bottomRect.Left)
            {
                distanceOutlines.Add(new DistanceOutline(topRect.BottomLeft(), bottomRect.TopLeft(), isDistanceLine: false, isAlignmentLine: true));
            }

            if (topRect.Right == bottomRect.Right)
            {
                distanceOutlines.Add(new DistanceOutline(topRect.BottomRight(), bottomRect.TopRight(), isDistanceLine: false, isAlignmentLine: true));
            }

            return distanceOutlines;
        }
    }
}
