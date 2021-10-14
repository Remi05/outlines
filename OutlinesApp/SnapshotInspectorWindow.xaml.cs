using System.Windows;
using Outlines.Core;
using OutlinesApp.Services;
using OutlinesApp.ViewModels;

namespace OutlinesApp
{
    public partial class SnapshotInspectorWindow : Window
    {
        public SnapshotInspectorWindow(Snapshot snapshot)
        {
            InitializeComponent();

            IDistanceOutlinesProvider distanceOutlinesProvider = new DistanceOutlinesProvider();
            IElementProvider elementProvider = new CachedElementProvider(snapshot.UITree);
            IOutlinesService outlinesService = new OutlinesService(distanceOutlinesProvider, elementProvider);
            ICoordinateConverter coordinateConverter = new CachedCoordinateConverter(snapshot);
            IScreenHelper screenHelper = new ScreenHelper(this);
            IUITreeService uiTreeService = new CachedUITreeService(snapshot.UITree);

            OverlayViewModel overlayViewModel = new OverlayViewModel(Dispatcher, outlinesService, coordinateConverter, screenHelper);
            PropertiesViewModel propertiesViewModel = new PropertiesViewModel(outlinesService);
            UITreeViewModel uiTreeViewModel = new UITreeViewModel(Dispatcher, outlinesService, uiTreeService);
            SnapshotInspectorViewModel snapshotInspectorViewModel = new SnapshotInspectorViewModel(outlinesService, coordinateConverter);
            snapshotInspectorViewModel.Snapshot = snapshot;

            var serviceContainer = ServiceContainer.Instance;
            serviceContainer.AddService(typeof(OverlayViewModel), overlayViewModel);
            serviceContainer.AddService(typeof(PropertiesViewModel), propertiesViewModel);
            serviceContainer.AddService(typeof(SnapshotInspectorViewModel), snapshotInspectorViewModel);
            serviceContainer.AddService(typeof(UITreeViewModel), uiTreeViewModel);
        }
    }
}
