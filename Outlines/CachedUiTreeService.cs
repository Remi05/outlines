using System;
using System.Drawing;

namespace Outlines
{
    public class CachedUiTreeService : IUiTreeService
    {
        public UiTreeNode RootNode { get; private set; }

        public event RootNodeChangedEventHandler RootNodeChanged;

        public CachedUiTreeService(UiTreeNode rootNode)
        {
            RootNode = rootNode;
        }

        public UiTreeNode GetSubTree(ElementProperties rootElementProperties)
        {
            return RootNode != null ? GetSubTree(rootElementProperties, RootNode) : null;
        }

        private UiTreeNode GetSubTree(ElementProperties rootElementProperties, UiTreeNode curNode)
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

        public UiTreeNode GetSubTreeInBounds(Rectangle bounds)
        {
            throw new NotImplementedException();
        }
    }
}
