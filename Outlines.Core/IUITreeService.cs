using System.Drawing;

namespace Outlines.Core
{
    public delegate void RootNodeChangedEventHandler();

    public interface IUITreeService
    {
        IUITreeNode RootNode { get; }

        CachedUITreeNode CreateSnapshotOfElementSubTree(ElementProperties rootElementProperties);
        CachedUITreeNode CreateSnapshotOfSubTreeInBounds(Rectangle bounds);
    }
}