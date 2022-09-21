using System.Collections.Generic;

namespace Outlines.Core
{
    public class CachedUITreeNode : IUITreeNode
    {
        public ElementProperties ElementProperties { get; set; }
        public bool HasChildren => (Children != null) && (Children.Count > 0);
        public List<CachedUITreeNode> Children { get; set; } = new List<CachedUITreeNode>();

        public event UITreeNodeChildrenChangedHandler ChildrenChanged;

        public IEnumerable<IUITreeNode> GetAndMonitorChildren() => Children;
    }
}
