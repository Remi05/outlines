using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                var childrenElements = rootElementProperties.Element.FindAll(TreeScope.Children, Condition.TrueCondition);
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
    }
}
