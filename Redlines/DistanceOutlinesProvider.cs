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
                Rect containedRect = isSelectedContained ? selectedElement.BoundingRect : targetElement.BoundingRect;
                Rect containingRect = isSelectedContained ? targetElement.BoundingRect : selectedElement.BoundingRect;

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

                distanceOutlines.Add(topToTopOutline);
                distanceOutlines.Add(bottomToBottomOutline);
                distanceOutlines.Add(leftToLeftOutline);
                distanceOutlines.Add(rightToRightOutline);
            }
            else
            {
                Vector selectedDiag = Point.Subtract(selectedElement.BoundingRect.BottomRight, selectedElement.BoundingRect.TopLeft);
                Point selectedCenter = Point.Add(selectedElement.BoundingRect.TopLeft, selectedDiag / 2);

                // Vertical
                if (selectedElement.BoundingRect.Top > targetElement.BoundingRect.Bottom)
                {
                    // Top to Bottom
                    var selectedTopCenter = new Point(selectedCenter.X, selectedElement.BoundingRect.Top);
                    var topToBottomOutline = new DistanceOutline(selectedTopCenter, new Point(selectedCenter.X, targetElement.BoundingRect.Bottom));
                    distanceOutlines.Add(topToBottomOutline);
                }
                else if (selectedElement.BoundingRect.Bottom < targetElement.BoundingRect.Top)
                {
                    // Bottom to Top
                    var selectedBottomCenter = new Point(selectedCenter.X, selectedElement.BoundingRect.Bottom);
                    var bottomToTopOutline = new DistanceOutline(selectedBottomCenter, new Point(selectedCenter.X, targetElement.BoundingRect.Top));
                    distanceOutlines.Add(bottomToTopOutline);
                }

                // Horizontal
                if (selectedElement.BoundingRect.Left > targetElement.BoundingRect.Right)
                {
                    // Left to Right
                    var selectedLeftCenter = new Point(selectedElement.BoundingRect.Left, selectedCenter.Y);
                    var leftToRightOutline = new DistanceOutline(selectedLeftCenter, new Point(targetElement.BoundingRect.Right, selectedCenter.Y));
                    distanceOutlines.Add(leftToRightOutline);
                }
                else if (selectedElement.BoundingRect.Right < targetElement.BoundingRect.Left)
                {
                    // Right to Left
                    var selectedRightCenter = new Point(selectedElement.BoundingRect.Right, selectedCenter.Y);
                    var rightToLeftOutline = new DistanceOutline(selectedRightCenter, new Point(targetElement.BoundingRect.Left, selectedCenter.Y));
                    distanceOutlines.Add(rightToLeftOutline);
                }
            }

            return distanceOutlines;
        }

    }
}
