﻿using System;
using System.Drawing;

namespace Outlines.Core
{
    public class CachedElementProvider : IElementProvider
    {
        private CachedUITreeNode UITree { get; set; }

        public CachedElementProvider(CachedUITreeNode uiTree)
        {
            UITree = uiTree;
        }

        public ElementProperties TryGetElementFromPoint(Point point)
        {
            return GetContainingElement(UITree, point); 
        }

        private ElementProperties GetContainingElement(CachedUITreeNode rootNode, Point point)
        {
            Rectangle elementBounds = rootNode.ElementProperties.BoundingRect;

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

            if (!elementBounds.Contains(point))
            {
                return null;
            }

            return rootNode.ElementProperties;
        }

    }
}
