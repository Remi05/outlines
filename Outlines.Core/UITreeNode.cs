using System.Collections.Generic;

namespace Outlines.Core
{
    public class UITreeNode
    {
        public ElementProperties ElementProperties { get; set; }
        public List<UITreeNode> Children { get; set; } = new List<UITreeNode>();
    }
}
