using System;
using System.Drawing;

namespace Outlines.Core
{
    public class CachedUITreeService : IUITreeService
    {
        private CachedUITreeNode RootCachedNode { get; }
        public IUITreeNode RootNode => RootCachedNode;

        public CachedUITreeService(CachedUITreeNode rootNode)
        {
            RootCachedNode = rootNode;
        }

        public CachedUITreeNode CreateSnapshotOfElementSubTree(ElementProperties rootElementProperties)
        {
            return (RootCachedNode != null) ? CreateSnapshotOfElementSubTree(rootElementProperties, RootCachedNode) : null;
        }

        private CachedUITreeNode CreateSnapshotOfElementSubTree(ElementProperties rootElementProperties, CachedUITreeNode curNode)
        {
            if (rootElementProperties == curNode.ElementProperties)
            {
                return curNode;
            }

            foreach (var child in curNode.Children)
            {
                var subtree = CreateSnapshotOfElementSubTree(rootElementProperties, child);
                if (subtree != null)
                {
                    return subtree;
                }
            }
            return null;
        }

        public CachedUITreeNode CreateSnapshotOfSubTreeInBounds(Rectangle bounds)
        {
            throw new NotImplementedException();
        }
    }
}
