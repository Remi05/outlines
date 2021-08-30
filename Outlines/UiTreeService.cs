using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;

namespace Outlines
{
    public delegate void RootNodeChangedEventHandler();

    public class UiTreeService : IUiTreeService
    {
        private const int MaxTreeDepth = int.MaxValue;
        private const int StartupTreeDepth = 2;
        private const int FollowUpTreeDepth1 = 4;
        private const int FollowUpTreeDepth2 = 10;

        private IElementPropertiesProvider ElementPropertiesProvider { get; set; }
        private IOutlinesService OutlinesService { get; set; }

        private Condition FilterCondition { get; set; }

        private UiTreeNode rootNode = null;
        public UiTreeNode RootNode
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

        public UiTreeService(IElementPropertiesProvider elementPropertiesProvider, IOutlinesService outlinesService)
        {
            if (elementPropertiesProvider == null || outlinesService == null)
            {
                throw new ArgumentNullException(elementPropertiesProvider == null ? nameof(elementPropertiesProvider) : nameof(outlinesService));
            }
            ElementPropertiesProvider = elementPropertiesProvider;
            OutlinesService = outlinesService;
            FilterCondition = new PropertyCondition(AutomationElement.IsOffscreenProperty, false);
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
                var rootElementProperties = ElementPropertiesProvider.GetElementProperties(AutomationElement.RootElement);
                RootNode = GetSubTree(rootElementProperties, treeDepth);
            });
        }

        public UiTreeNode GetSubTree(ElementProperties rootElementProperties)
        {
            return GetSubTree(rootElementProperties, MaxTreeDepth);
        }

        private UiTreeNode GetSubTree(ElementProperties rootElementProperties, int treeDepth)
        {
            if (rootElementProperties == null)
            {
                return null;
            }
            try
            {
                var childrenElements = rootElementProperties.Element.FindAll(TreeScope.Children, FilterCondition);
                var childrenNodes = new List<UiTreeNode>();
                if (treeDepth > 1)
                {
                    foreach (var child in childrenElements)
                    {
                        ElementProperties childElementProperties = ElementPropertiesProvider.GetElementProperties(child as AutomationElement);
                        UiTreeNode childNode = GetSubTree(childElementProperties, treeDepth - 1);
                        if (childNode != null)
                        {
                            childrenNodes.Add(childNode);
                        }
                    }
                }
                return new UiTreeNode() { ElementProperties = rootElementProperties, Children = childrenNodes };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public UiTreeNode GetSubTreeInBounds(Rectangle bounds)
        {
            var rect = new Rect(bounds.X, bounds.Y, bounds.Width, bounds.Height);
            var childrenNodes = GetSubTreeInBounds(rect, AutomationElement.RootElement);
            var rootProperties = new ElementProperties() { Name = "Root", BoundingRect = rect };
            return new UiTreeNode() { ElementProperties = rootProperties, Children = childrenNodes };
        }

        private List<UiTreeNode> GetSubTreeInBounds(Rect bounds, AutomationElement curElement)
        {
            var elementsInBounds = new List<UiTreeNode>();

            try
            {
                if (bounds.Contains(curElement.Current.BoundingRectangle))
                {
                    var childrenElements = curElement.FindAll(TreeScope.Children, FilterCondition);
                    var childrenNodes = new List<UiTreeNode>();

                    foreach (var child in childrenElements)
                    {
                        ElementProperties childElementProperties = ElementPropertiesProvider.GetElementProperties(child as AutomationElement);
                        UiTreeNode childNode = GetSubTree(childElementProperties, MaxTreeDepth);
                        if (childNode != null)
                        {
                            childrenNodes.Add(childNode);
                        }
                    }

                    ElementProperties curElementProperties = ElementPropertiesProvider.GetElementProperties(curElement);
                    UiTreeNode curNode = new UiTreeNode() { ElementProperties = curElementProperties, Children = childrenNodes };
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
