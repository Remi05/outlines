using System.Windows;

namespace Outlines
{
    public interface IUiTreeService
    {
        UiTreeNode RootNode { get; }

        event RootNodeChangedEventHandler RootNodeChanged;

        UiTreeNode GetSubTree(ElementProperties rootElementProperties);
        UiTreeNode GetSubTreeInBounds(Rect bounds);
    }
}