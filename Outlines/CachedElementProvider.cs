using System;
using System.Windows;

namespace Outlines
{
    public class CachedElementProvider : IElementProvider
    {
        public UiTreeNode UiTree { get; set; }

        public CachedElementProvider(UiTreeNode uiTree)
        {
            UiTree = uiTree;
        }

        public ElementProperties TryGetElementFromPoint(Point point)
        {
            return GetContainingElement(UiTree, point); 
        }

        private ElementProperties GetContainingElement(UiTreeNode rootNode, Point point)
        {
            Rect elementBounds = rootNode.ElementProperties.BoundingRect;

            if (!elementBounds.Contains(point))
            {
                return null;
            }

            var children = rootNode.Children;
            foreach (var child in children)
            {
                try
                {
                    var containingElement = GetContainingElement(child, point);
                    if (containingElement != null)
                    {
                        return containingElement;
                    }
                }
                catch (Exception) { }
            }

            return rootNode.ElementProperties;
        }

    }
}
