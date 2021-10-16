using System;
using System.Drawing;

namespace Outlines.Core
{
    public class CachedElementProvider : IElementProvider
    {
        private UITreeNode UITree { get; set; }

        public CachedElementProvider(UITreeNode uiTree)
        {
            UITree = uiTree;
        }

        public ElementProperties TryGetElementFromPoint(Point point)
        {
            return GetContainingElement(UITree, point); 
        }

        public ElementProperties TryGetElementFromHandle(IntPtr handle)
        {
            return GetElementFromHandle(UITree, handle.ToInt32());
        }

        private ElementProperties GetContainingElement(UITreeNode rootNode, Point point)
        {
            if (rootNode == null)
            {
                return null;
            }

            Rectangle elementBounds = rootNode.ElementProperties.BoundingRect;

            if (!elementBounds.Contains(point))
            {
                return null;
            }

            foreach (var child in rootNode.Children)
            {
                var containingElement = GetContainingElement(child, point);
                if (containingElement != null)
                {
                    return containingElement;
                }
            }

            return rootNode.ElementProperties;
        }

        private ElementProperties GetElementFromHandle(UITreeNode rootNode, int handle)
        {
            if (rootNode != null)
            {
                if (rootNode.ElementProperties.NativeWindowHandle == handle)
                {
                    return UITree.ElementProperties;
                }

                foreach (var child in rootNode.Children)
                {
                    var element = GetElementFromHandle(child, handle);
                    if (element != null)
                    {
                        return element;
                    }
                }
            }
            return null;
        }
    }
}
