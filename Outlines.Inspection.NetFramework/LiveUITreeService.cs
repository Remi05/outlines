using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Automation;
using Outlines.Core;

namespace Outlines.Inspection.NetFramework
{
    public class LiveUITreeService : IUITreeService
    {
        private const int MaxTreeDepth = int.MaxValue;
        private const int StartupTreeDepth = 2;
        private const int FollowUpTreeDepth1 = 4;
        private const int FollowUpTreeDepth2 = 10;

        private IElementPropertiesProvider ElementPropertiesProvider { get; set; }
        private IOutlinesService OutlinesService { get; set; }

        private Condition FilterCondition { get; set; }

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

        public LiveUITreeService(IElementPropertiesProvider elementPropertiesProvider, IOutlinesService outlinesService)
        {
            if (elementPropertiesProvider == null || outlinesService == null)
            {
                throw new ArgumentNullException(elementPropertiesProvider == null ? nameof(elementPropertiesProvider) : nameof(outlinesService));
            }
            ElementPropertiesProvider = elementPropertiesProvider;
            OutlinesService = outlinesService;
            FilterCondition = new AndCondition(new NotCondition(new AndCondition(new PropertyCondition(AutomationElement.NameProperty, "Outlines", PropertyConditionFlags.IgnoreCase),
                                                                                 new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window))),
                                               new PropertyCondition(AutomationElement.IsOffscreenProperty, false));
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
                var rootElementProperties = ElementPropertiesProvider.GetElementProperties(AutomationElement.RootElement) as AutomationElementProperties;
                RootNode = GetSubTree(rootElementProperties, treeDepth);
            });
        }

        public UITreeNode GetSubTree(ElementProperties rootElementProperties)
        {
            return GetSubTree(rootElementProperties as AutomationElementProperties, MaxTreeDepth);
        }

        private UITreeNode GetSubTree(AutomationElementProperties rootElementProperties, int treeDepth)
        {
            if (rootElementProperties == null)
            {
                return null;
            }
            try
            {
                var childrenElements = rootElementProperties.Element.FindAll(TreeScope.Children, FilterCondition);
                var childrenNodes = new List<UITreeNode>();
                if (treeDepth > 1)
                {
                    foreach (var child in childrenElements)
                    {
                        var childElementProperties = ElementPropertiesProvider.GetElementProperties(child as AutomationElement) as AutomationElementProperties;
                        UITreeNode childNode = GetSubTree(childElementProperties, treeDepth - 1);
                        if (childNode != null)
                        {
                            childrenNodes.Add(childNode);
                        }
                    }
                }
                return new UITreeNode() { ElementProperties = rootElementProperties, Children = childrenNodes };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public UITreeNode GetSubTreeInBounds(Rectangle bounds)
        {
            var childrenNodes = GetSubTreeInBounds(bounds, AutomationElement.RootElement);
            var rootProperties = new ElementProperties() { Name = "Root", BoundingRect = bounds };
            return new UITreeNode() { ElementProperties = rootProperties, Children = childrenNodes };
        }

        private List<UITreeNode> GetSubTreeInBounds(Rectangle bounds, AutomationElement curElement)
        {
            var elementsInBounds = new List<UITreeNode>();

            try
            {
                if (bounds.Contains(curElement.Current.BoundingRectangle.ToDrawingRectangle()))
                {
                    var childrenElements = curElement.FindAll(TreeScope.Children, FilterCondition);
                    var childrenNodes = new List<UITreeNode>();

                    foreach (var child in childrenElements)
                    {
                        var childElementProperties = ElementPropertiesProvider.GetElementProperties(child as AutomationElement) as AutomationElementProperties;
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
                    var childrenElements = curElement.FindAll(TreeScope.Children, FilterCondition);
                    foreach (var child in childrenElements)
                    {
                        var subTreeInBounds = GetSubTreeInBounds(bounds, child as AutomationElement);
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
