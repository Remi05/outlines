using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using UIAutomationClient;
using Outlines.Core;

namespace Outlines.Inspection
{
    public class LiveUITreeService : IUITreeService
    {
        private const int MaxTreeDepth = int.MaxValue;
        private const int StartupTreeDepth = 2;
        private const int FollowUpTreeDepth1 = 4;
        private const int FollowUpTreeDepth2 = 10;

        private IUIAutomation UIAutomation { get; set; } = new CUIAutomation();
        private IElementPropertiesProvider ElementPropertiesProvider { get; set; }
        private IOutlinesService OutlinesService { get; set; }
        private IIgnorableWindowsSource IgnorableWindowsSource { get; set; }

        private IUIAutomationCondition ConstantFilterCondition { get; set; }

        private UITreeNode rootNode = null;
        public UITreeNode RootNode
        {
            get => rootNode;
            private set
            {
                if (value != rootNode)
                {
                    rootNode = value;
                    RootNodeChanged?.Invoke();
                }
            }
        }

        public event RootNodeChangedEventHandler RootNodeChanged;

        public LiveUITreeService(IElementPropertiesProvider elementPropertiesProvider, IOutlinesService outlinesService, IIgnorableWindowsSource ignorableWindowsSource)
        {
            if (elementPropertiesProvider == null || outlinesService == null)
            {
                throw new ArgumentNullException(elementPropertiesProvider == null ? nameof(elementPropertiesProvider) : nameof(outlinesService));
            }
            ElementPropertiesProvider = elementPropertiesProvider;
            OutlinesService = outlinesService;
            IgnorableWindowsSource = ignorableWindowsSource;
            ConstantFilterCondition = UIAutomation.CreatePropertyCondition(UIA_PropertyIds.UIA_IsOffscreenPropertyId, false);
            InitializeUiTree();
        }

        private async Task InitializeUiTree()
        {
            // Loading the entire tree on startup is slow, so just show minimal results at first and add more async.
            await RefreshUiTree(StartupTreeDepth);
            await RefreshUiTree(FollowUpTreeDepth1);
            await RefreshUiTree(FollowUpTreeDepth2);
        }

        private async Task RefreshUiTree(int treeDepth)
        {
            await Task.Run(() =>
            {
                var rootElementProperties = ElementPropertiesProvider.GetElementProperties(UIAutomation.GetRootElement()) as AutomationElementProperties;
                RootNode = GetSubTree(rootElementProperties, treeDepth);
            });
        }

        public UITreeNode GetSubTree(ElementProperties rootElementProperties)
        {
            return GetSubTree(rootElementProperties as AutomationElementProperties, MaxTreeDepth);
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

        private UITreeNode GetSubTree(AutomationElementProperties rootElementProperties, int treeDepth)
        {
            if (rootElementProperties == null)
            {
                return null;
            }
            try
            {
                var childrenElements = rootElementProperties.Element.FindAll(TreeScope.TreeScope_Children, GetFilterCondition());
                var childrenNodes = new List<UITreeNode>();
                if (treeDepth > 1)
                {
                    for (int i = 0; i < childrenElements.Length; ++i)
                    {
                        var childElementProperties = ElementPropertiesProvider.GetElementProperties(childrenElements.GetElement(i)) as AutomationElementProperties;
                        UITreeNode childNode = GetSubTree(childElementProperties, treeDepth - 1);
                        if (childNode != null)
                        {
                            childrenNodes.Add(childNode);
                        }
                    }
                }
                return new UITreeNode() { ElementProperties = rootElementProperties, Children = childrenNodes };
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public UITreeNode GetSubTreeInBounds(Rectangle bounds)
        {
            var childrenNodes = GetSubTreeInBounds(bounds, UIAutomation.GetRootElement());
            var rootProperties = new ElementProperties() { Name = "Root", BoundingRect = bounds };
            return new UITreeNode() { ElementProperties = rootProperties, Children = childrenNodes };
        }

        private List<UITreeNode> GetSubTreeInBounds(Rectangle bounds, IUIAutomationElement curElement)
        {
            var elementsInBounds = new List<UITreeNode>();

            try
            {
                Rectangle curElementBounds = curElement.CurrentBoundingRectangle.ToDrawingRectangle();
                if (bounds.IntersectsWith(curElementBounds))
                {
                    var childrenElements = curElement.FindAll(TreeScope.TreeScope_Children, GetFilterCondition());
                    var childrenNodes = new List<UITreeNode>();

                    for (int i = 0; i < childrenElements.Length; ++i)
                    {
                        var childElementProperties = ElementPropertiesProvider.GetElementProperties(childrenElements.GetElement(i)) as AutomationElementProperties;
                        UITreeNode childNode = GetSubTree(childElementProperties, MaxTreeDepth);
                        if (childNode != null)
                        {
                            childrenNodes.Add(childNode);
                        }
                    }

                    ElementProperties curElementProperties = ElementPropertiesProvider.GetElementProperties(curElement);
                    UITreeNode curNode = new UITreeNode() { ElementProperties = curElementProperties, Children = childrenNodes };
                    elementsInBounds.Add(curNode);
                }
                else
                {
                    var childrenElements = curElement.FindAll(TreeScope.TreeScope_Children, GetFilterCondition());
                    for (int i = 0; i < childrenElements.Length; ++i)
                    {
                        var subTreeInBounds = GetSubTreeInBounds(bounds, childrenElements.GetElement(i));
                        foreach (var childNode in subTreeInBounds)
                        {
                            elementsInBounds.Add(childNode);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            return elementsInBounds;
        }
    }
}
