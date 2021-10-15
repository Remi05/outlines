using System.Drawing;

namespace Outlines.Core
{
    public delegate void RootNodeChangedEventHandler();

    public interface IUITreeService
    {
        UITreeNode RootNode { get; }

        event RootNodeChangedEventHandler RootNodeChanged;

        UITreeNode GetSubTree(ElementProperties rootElementProperties);
        UITreeNode GetSubTreeInBounds(Rectangle bounds);
    }
}