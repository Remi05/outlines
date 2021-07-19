using System;
using System.Collections.Generic;
using System.Windows.Automation;

namespace Outlines
{
    public delegate void RootNodeChangedEventHandler();

    public class UiTreeService : IUiTreeService
    {
        private const int DefaultMaxTreeDepth = 4;

        private IElementPropertiesProvider ElementPropertiesProvider { get; set; }
        private IOutlinesService OutlinesService { get; set; }

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
            OutlinesService.SelectedElementChanged += UpdateRootNode;
            UpdateRootNode();
        }

        private void UpdateRootNode()
        {
            var selectedElementProperties = OutlinesService.SelectedElementProperties;
            if (selectedElementProperties != null)
            {
                RootNode = GetTreeNode(selectedElementProperties);
            }
            else
            {
                var rootElementProperties = ElementPropertiesProvider.GetElementProperties(AutomationElement.RootElement);
                RootNode = GetTreeNode(rootElementProperties);
            }
        }

        private UiTreeNode GetTreeNode(ElementProperties rootElementProperties, int remainingDepth = DefaultMaxTreeDepth)
        {
            if (rootElementProperties == null)
            {
                return null;
            }
            try
            {
                var childrenElements = rootElementProperties.Element.FindAll(TreeScope.Children, Condition.TrueCondition);
                var childrenNodes = new List<UiTreeNode>();
                if (remainingDepth > 0)
                {
                    foreach (var child in childrenElements)
                    {
                        ElementProperties childElementProperties = ElementPropertiesProvider.GetElementProperties(child as AutomationElement);
                        UiTreeNode childNode = GetTreeNode(childElementProperties, remainingDepth - 1);
                        if (childNode != null)
                        {
                            childrenNodes.Add(childNode);
                        }
                    }
                }
                return new UiTreeNode(rootElementProperties, childrenNodes);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
