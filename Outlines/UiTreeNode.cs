using System;
using System.Collections.Generic;

namespace Outlines
{
    public class UiTreeNode
    {
        public ElementProperties ElementProperties { get; private set; }
        public List<UiTreeNode> Children { get; private set; }

        public UiTreeNode(ElementProperties elementProperties, List<UiTreeNode> children = null)
        {
            if (elementProperties == null)
            {
                throw new ArgumentNullException(nameof(elementProperties));
            }
            ElementProperties = elementProperties;
            Children = children ?? new List<UiTreeNode>();
        }
    }
}
