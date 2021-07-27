using System.Collections.Generic;

namespace Outlines
{
    public class UiTreeNode
    {
        public ElementProperties ElementProperties { get; set; }
        public List<UiTreeNode> Children { get; set; } = new List<UiTreeNode>();
    }
}
