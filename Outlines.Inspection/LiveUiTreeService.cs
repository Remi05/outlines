using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UIAutomationClient;
using Outlines.Core;

namespace Outlines.Inspection
{
    public class LiveUITreeService : IUITreeService
    {
        private IUIAutomation UIAutomation { get; } = new CUIAutomation();
        private IElementPropertiesProvider ElementPropertiesProvider { get; }
        private IIgnorableWindowsSource IgnorableWindowsSource { get; }
        private IUIAutomationCondition ConstantFilterCondition { get; }
        public IUITreeNode RootNode { get; }

        public LiveUITreeService(IElementPropertiesProvider elementPropertiesProvider, IIgnorableWindowsSource ignorableWindowsSource)
        {
            if (elementPropertiesProvider == null)
            {
                throw new ArgumentNullException(nameof(elementPropertiesProvider));
            }
            ElementPropertiesProvider = elementPropertiesProvider;
            IgnorableWindowsSource = ignorableWindowsSource;
            ConstantFilterCondition = UIAutomation.CreatePropertyCondition(UIA_PropertyIds.UIA_IsOffscreenPropertyId, false);
            RootNode = new LiveUITreeNode(UIAutomation.GetRootElement(), ElementPropertiesProvider, GetFilterCondition());
        }

        private IUIAutomationCondition GetFilterCondition()
        {
            var windowsToIgnore = IgnorableWindowsSource?.GetWindowsToIgnore();
            if (windowsToIgnore == null || windowsToIgnore.Count == 0)
            {
                return ConstantFilterCondition;
            }

            var windowConditions = windowsToIgnore.Select(window => UIAutomation.CreatePropertyCondition(UIA_PropertyIds.UIA_NativeWindowHandlePropertyId, window));
            var ignoredWindowsCondition = UIAutomation.CreateNotCondition(UIAutomation.CreateOrConditionFromArray(windowConditions.ToArray()));
            return UIAutomation.CreateAndCondition(ConstantFilterCondition, ignoredWindowsCondition);
        }

        public CachedUITreeNode CreateSnapshotOfElementSubTree(ElementProperties rootElementProperties)
        {
            AutomationElementProperties rootAutomationElementProperties = rootElementProperties as AutomationElementProperties;
            if (rootAutomationElementProperties == null)
            {
                return null;
            }
            try
            {
                var childrenElements = rootAutomationElementProperties.Element.FindAll(TreeScope.TreeScope_Children, GetFilterCondition());
                var childrenNodes = new List<CachedUITreeNode>();

                for (int i = 0; i < childrenElements.Length; ++i)
                {
                    var childElementProperties = ElementPropertiesProvider.GetElementProperties(childrenElements.GetElement(i));
                    CachedUITreeNode childNode = CreateSnapshotOfElementSubTree(childElementProperties);
                    if (childNode != null)
                    {
                        childrenNodes.Add(childNode);
                    }
                }

                return new CachedUITreeNode() { ElementProperties = rootAutomationElementProperties, Children = childrenNodes };
            }
            catch
            {
                // TODO: Consider logging the failure to create the subtree.
                return null;
            }
        }

        public CachedUITreeNode CreateSnapshotOfSubTreeInBounds(Rectangle bounds)
        {
            var childrenNodes = CreateSnapshotOfSubTreeInBounds(bounds, UIAutomation.GetRootElement());
            var rootProperties = new ElementProperties() { Name = "Root", BoundingRect = bounds };
            return new CachedUITreeNode() { ElementProperties = rootProperties, Children = childrenNodes };
        }

        private List<CachedUITreeNode> CreateSnapshotOfSubTreeInBounds(Rectangle bounds, IUIAutomationElement curElement)
        {
            var elementsInBounds = new List<CachedUITreeNode>();
            try
            {
                Rectangle curElementBounds = curElement.CurrentBoundingRectangle.ToDrawingRectangle();
                if (bounds.IntersectsWith(curElementBounds))
                {
                    var childrenElements = curElement.FindAll(TreeScope.TreeScope_Children, GetFilterCondition());
                    var childrenNodes = new List<CachedUITreeNode>();

                    for (int i = 0; i < childrenElements.Length; ++i)
                    {
                        var childElementProperties = ElementPropertiesProvider.GetElementProperties(childrenElements.GetElement(i));
                        CachedUITreeNode childNode = CreateSnapshotOfElementSubTree(childElementProperties);
                        if (childNode != null)
                        {
                            childrenNodes.Add(childNode);
                        }
                    }

                    ElementProperties curElementProperties = ElementPropertiesProvider.GetElementProperties(curElement);
                    CachedUITreeNode curNode = new CachedUITreeNode() { ElementProperties = curElementProperties, Children = childrenNodes };
                    elementsInBounds.Add(curNode);
                }
                else
                {
                    var childrenElements = curElement.FindAll(TreeScope.TreeScope_Children, GetFilterCondition());
                    for (int i = 0; i < childrenElements.Length; ++i)
                    {
                        var subTreeInBounds = CreateSnapshotOfSubTreeInBounds(bounds, childrenElements.GetElement(i));
                        foreach (var childNode in subTreeInBounds)
                        {
                            elementsInBounds.Add(childNode);
                        }
                    }
                }
            }
            catch
            {
                // TODO: Consider logging the failure to create the subtree.
                return null;
            }

            return elementsInBounds;
        }
    }
}
