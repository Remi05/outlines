using System.Collections.Generic;

namespace Outlines.Core
{
    public delegate void UITreeNodeChildrenChangedHandler();

    public interface IUITreeNode
    {
        ElementProperties ElementProperties { get; }
        bool HasChildren { get; }

        event UITreeNodeChildrenChangedHandler ChildrenChanged;

        IEnumerable<IUITreeNode> GetAndMonitorChildren();
    }
}