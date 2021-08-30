namespace Outlines
{
    public interface IUiTreeService
    {
        UiTreeNode RootNode { get; }

        event RootNodeChangedEventHandler RootNodeChanged;
    }
}