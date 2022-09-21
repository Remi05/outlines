using System;
using System.Collections.Generic;
using UIAutomationClient;
using Outlines.Core;

namespace Outlines.Inspection
{
    internal class LiveUITreeNode : IUITreeNode
    {
        private IUIAutomation UIAutomation { get; set; } = new CUIAutomation();
        private IElementPropertiesProvider ElementPropertiesProvider { get; set; }

        private IUIAutomationElement AutomationElement { get; set; }
        public ElementProperties ElementProperties { get; private set; }
        public bool HasChildren { get; private set; }

        private bool AreChildrenMonitored { get; set; } = false;
        private List<IUITreeNode> Children { get; set; }
        private IUIAutomationCondition ChildrenFilterCondition { get; set; }
        private IUIAutomationStructureChangedEventHandler StructureChangedHandler { get; set; }

        public event UITreeNodeChildrenChangedHandler ChildrenChanged;

        public LiveUITreeNode(IUIAutomationElement automationElement, IElementPropertiesProvider elementPropertiesProvider, IUIAutomationCondition childrenFilterCondition)
        {
            AutomationElement = automationElement;
            ElementPropertiesProvider = elementPropertiesProvider;
            ElementProperties = ElementPropertiesProvider.GetElementProperties(automationElement) as AutomationElementProperties;
            ChildrenFilterCondition = childrenFilterCondition;
            HasChildren = CheckIfElementHasChildren();
        }

        ~LiveUITreeNode()
        {
            if (StructureChangedHandler != null)
            {
                try
                {
                    UIAutomation.RemoveStructureChangedEventHandler(AutomationElement, StructureChangedHandler);
                }
                catch
                {
                    // TODO: Consider logging the failure to remove the StructureChangedEventHandler.
                }
            }
        }

        private bool CheckIfElementHasChildren()
        {
            try
            {
                return (AutomationElement.FindFirst(TreeScope.TreeScope_Children, ChildrenFilterCondition) != null);
            }
            catch
            {
                // If we fail to find any child, it is not critical so we shouldn't throw, we can simply assume there are no children.
                return false;
            }
        }

        public IEnumerable<IUITreeNode> GetAndMonitorChildren()
        {
            // We defer finding and monitoring children elements until GetAndMonitorChildren() is called since loading
            // loading the entire UI tree is resource intensive so we only load and monitor the parts of it that are needed.
            if (!AreChildrenMonitored)
            {
                StartMonitoringChildren();
                UpdateChildren();
            }
            return Children;
        }

        private void StartMonitoringChildren()
        {
            AreChildrenMonitored = true;
            StructureChangedHandler = new UIAutomationStructureChangedEventHandler((sender) => UpdateChildren());
            try
            {
                UIAutomation.AddStructureChangedEventHandler(AutomationElement, TreeScope.TreeScope_Children, null, StructureChangedHandler);
            }
            catch
            {
                // TODO: Consider logging the failure to monitor children elements.
            }
        }

        private void UpdateChildren()
        {
            var newChildrenNodes = new List<IUITreeNode>();
            try
            {
                var childrenElements = AutomationElement.FindAll(TreeScope.TreeScope_Children, ChildrenFilterCondition);
                if (childrenElements != null)
                {
                    for (int i = 0; i < childrenElements.Length; ++i)
                    {
                        var childNode = new LiveUITreeNode(childrenElements.GetElement(i), ElementPropertiesProvider, ChildrenFilterCondition);
                        newChildrenNodes.Add(childNode);
                    }
                }
            }
            catch
            {
                // TODO: Consider logging the failure to find children elements.
            }
            finally
            {
                Children = newChildrenNodes;
                ChildrenChanged?.Invoke();
            }
        }
    }
}
