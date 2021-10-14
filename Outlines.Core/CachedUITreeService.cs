using System;
using System.Drawing;

namespace Outlines.Core
{
    public class CachedUITreeService : IUITreeService
    {
        public UITreeNode RootNode { get; private set; }

        public event RootNodeChangedEventHandler RootNodeChanged;

        public CachedUITreeService(UITreeNode rootNode)
        {
            RootNode = rootNode;
        }

        public UITreeNode GetSubTree(ElementProperties rootElementProperties)
        {
            return RootNode != null ? GetSubTree(rootElementProperties, RootNode) : null;
        }

        private UITreeNode GetSubTree(ElementProperties rootElementProperties, UITreeNode curNode)
        {
            if (rootElementProperties == curNode.ElementProperties)
            {
                return curNode;
            }

            foreach (var child in curNode.Children)
            {
                var subtree = GetSubTree(rootElementProperties, child);
                if (subtree != null)
                {
                    return subtree;
                }
            }
            return null;
        }

        public UITreeNode GetSubTreeInBounds(Rectangle bounds)
        {
            throw new NotImplementedException();
        }
    }
}
